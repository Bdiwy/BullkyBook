using BullkyBook.Models;
using DataBase;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace BullkyBook.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;

        // ✅ Corrected constructor name
        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Profile()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveProfile(string FirstName, string LastName, string Email, string Phone)
        {
            // Save logic here
            TempData["Message"] = "Profile updated successfully!";
            return RedirectToAction("Profile");
        }
    }
}
