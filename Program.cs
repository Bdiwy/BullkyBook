using DataBase;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using BullkyBook.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

var supportedCultures = new[]
{
    new CultureInfo("en"),
    new CultureInfo("ar")
};

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("en");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.RequestCultureProviders = new IRequestCultureProvider[]
    {
        new RouteDataRequestCultureProvider()
        {
            RouteDataStringKey = "culture",
            UIRouteDataStringKey = "culture"
        },
        new CookieRequestCultureProvider()
    };
});

builder.Services.AddScoped<BookCategoryService>();
builder.Services.AddScoped<WeatherService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

var localizationOptions = app.Services
    .GetRequiredService<Microsoft.Extensions.Options.IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(localizationOptions.Value);

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "localized",
    pattern: "{culture=en}/{controller=Home}/{action=Index}/{id?}"
).WithStaticAssets();

app.MapFallbackToController("NotFound", "Home");

app.Run();