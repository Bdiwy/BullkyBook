using BullkyBook.Models;
using DataBase;
using Microsoft.AspNetCore.Mvc;
namespace BullkyBook.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> obj = _db.Catagories.ToList();
            return View(obj);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}