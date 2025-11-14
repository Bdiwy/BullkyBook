using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BullkyBook.Models;
using DataBase;
using X.PagedList;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BullkyBook.Controllers;

public class ProductController : Controller
{
    private readonly ApplicationDbContext _db;

    public ProductController(ApplicationDbContext db)
    {
        _db = db;
    }


    public IActionResult Index(int? page, int? pageSize)
    {
        int pageNumber = page ?? 1;
        int currentPageSize = pageSize ?? 5;

        var products = _db.Products
                                    .Include(p => p.Category)
                                    .OrderBy(c => c.Id)
                                    .ToPagedList(pageNumber, currentPageSize);

        ViewBag.PageSize = currentPageSize;
        ViewBag.PageSizes = new List<int> { 3, 5, 8, 10, 15, 20, 25, 30, 35, 40, 50, 60, 100 };

        return View(products);
    }

    public IActionResult Create()
    {
        ViewBag.Categories = _db.Catagories.OrderBy(c => c.Name).ToList();
        return View();
    }

    public async Task<IActionResult>  Store(Product? product)
    {
        if(ModelState.IsValid)
        {
            await _db.Products.AddAsync(product);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        else
        {
            ViewBag.Categories = _db.Catagories.OrderBy(c => c.Name).ToList();
            return View("Create", product);
        }
    }


    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || id == 0)
            return NotFound();
        var product = await _db.Products
                                .Include(p => p.Category)
                                .FirstOrDefaultAsync(p => p.Id == id);
        ViewBag.Categories = await _db.Catagories.OrderBy(c => c.Name).ToListAsync();
        if(product == null)
            return NotFound();
        return View(product);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(Product model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = await _db.Catagories.OrderBy(c => c.Name).ToListAsync();
            return View("Edit", model);
        }

        var product = await _db.Products.FindAsync(model.Id);
        if (product == null) return NotFound();

        product.Name = model.Name;
        product.QtyInStock = model.QtyInStock;
        product.CategoryId = model.CategoryId;

        await _db.SaveChangesAsync();
        return RedirectToAction("Index");
    }


    public async Task<IActionResult> Details(int? id)
    {
        if(id == null || id == 0 ) return NotFound();
        var product = await _db.Products.Include(p => p.Category).FirstOrDefaultAsync(p=> p.Id == id);
        if(product == null) return NotFound();
        return View("Details",product); 
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _db.Products.FindAsync(id);
        if (item == null) return Json(new { success = false, message = "Product not found!" });

        _db.Products.Remove(item);
        await _db.SaveChangesAsync();
        return Json(new { success = true, message = "Product deleted successfully!" });
    }
}