using BullkyBook.Models;
using BullkyBook.Services;
using DataBase;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
namespace BullkyBook.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        private readonly BookCategoryService _bookService ; 

        public CategoryController(ApplicationDbContext db , BookCategoryService bookService)
        {
            _db = db;
            this._bookService = bookService ; 
        }

        
        public IActionResult Index(int? page, int? pageSize)
        {
            int pageNumber = page ?? 1;
            int currentPageSize = pageSize ?? 5; // default to 5 if not selected

            var categories = _db.Catagories.OrderBy(c => c.Id).ToPagedList(pageNumber, currentPageSize);

            ViewBag.PageSize = currentPageSize; // so we can use it in the dropdown
            ViewBag.PageSizes = new List<int> { 3, 5, 8, 10, 15, 20, 25, 30, 35, 40, 50, 60, 100 };

            return View(categories);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null || id == 0)
                return Json(new { success = false, message = "Invalid ID" });

            var category = await _db.Catagories.FindAsync(id);
            if(category == null)
                return Json(new { success = false, message = "Category not found" });

            _db.Catagories.Remove(category);
            await _db.SaveChangesAsync();

            return Json(new { success = true, message = "Category deleted successfully" });
        }

        [HttpGet]
        [Route("api/fetch")]
        public async Task<IActionResult> ImportCategories()
        {
            try
            {
                bool success = await _bookService.FeatchAndImport();
                
                if (success)
                {
                    return Json(new { success = true, message = "Categories imported successfully" });
                }
                else
                {
                    return Json(new { success = false, message = "No new categories to import or import failed" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }
    }
}