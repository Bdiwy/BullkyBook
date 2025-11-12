using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BullkyBook.Models;
using DataBase;
using X.PagedList;
using Microsoft.EntityFrameworkCore;

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

    public IActionResult Store(Product? product)
    {
        if(ModelState.IsValid)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        else
        {
            return View("Create", product);
        }

    }







}