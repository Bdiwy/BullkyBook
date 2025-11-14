using BullkyBook.Models;
using DataBase;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http;
using System.Text.Json;

namespace BullkyBook.Services
{
    public class WeatherService
    {
        private readonly ApplicationDbContext _db;
        private readonly string Url = "https://api.openweathermap.org/data/2.5/forecast";

        public WeatherService(ApplicationDbContext db)
        {
            _db = db;
        }


        public async Task<object> GetWeatherAsync()
        {
            var weather = await GetWeatherDetailsAsync();
            if (weather == null)
            {
                return new { success = false, message = "Something went wrong!" };
            }

            var firstItem = weather.Value.GetProperty("list")[0];

            var temp = firstItem.GetProperty("main").GetProperty("temp").GetDouble();
            var feels = firstItem.GetProperty("main").GetProperty("feels_like").GetDouble();
            var desc = firstItem.GetProperty("weather")[0].GetProperty("description").GetString();
            var icon = firstItem.GetProperty("weather")[0].GetProperty("icon").GetString();
            var date = firstItem.GetProperty("dt_txt").GetString();

            return new
            {
                success = true,
                city = "Cairo",
                temp = temp,
                feels = feels,
                desc = desc,
                icon = icon,
                date = date
            };
        }

        

        private async Task<JsonElement?> GetWeatherDetailsAsync()
        {
            try
            {
                using var client = new HttpClient();

                var url = QueryHelpers.AddQueryString(
                    Url,
                    new Dictionary<string, string?>
                    {
                        ["appid"] = "97f476dee0cf24a3fd18a19988b3242c",
                        ["id"] = "360630",
                    });

                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var jsonDoc = JsonDocument.Parse(responseBody);

                return jsonDoc.RootElement;
            }
            catch
            {
                return null;
            }
        }

    }
}
