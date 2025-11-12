using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BullkyBook.Models;

namespace BullkyBook.Controllers;

public class AuthController : Controller
{
        [HttpGet("/login")]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpGet("/register")]
        public IActionResult Register()
        {
            return View("Register");
        }
}