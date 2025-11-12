using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BullkyBook.Models;
using DataBase;

namespace BullkyBook.Controllers;

public class ProductController : Controller
{
    private readonly ApplicationDbContext _db;

    public ProductController(ApplicationDbContext db)
    {
        _db = db;
    }


    public IActionResult Index()
    {
        var products = _db.Products.ToList();
        return View(products);
    }








}