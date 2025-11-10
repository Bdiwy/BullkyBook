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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Store(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Catagories.Add(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Create", category);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var category = await  _db.Catagories.FindAsync(id);
            if (category == null)
                return NotFound();
            return View(category);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var category = await _db.Catagories.FindAsync(id);
            if (category == null)
                return NotFound();
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Catagories.Update(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Edit", category);
        }

    }
}