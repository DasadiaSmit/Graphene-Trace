using Microsoft.AspNetCore.Mvc;
using WebApplication2.Data;
using WebApplication2.Models;
using System.Linq;

namespace WebApplication2.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor to connect DB
        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ============================================================
        // LOGIN (GET)
        // ============================================================
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // ============================================================
        // LOGIN (POST)
        // ============================================================
        [HttpPost]
        public IActionResult Login(string Email, string PasswordHash)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Email == Email && u.PasswordHash == PasswordHash && u.Role == "Patient");

            if (user != null)
            {
                // Store session data
                HttpContext.Session.SetString("UserEmail", user.Email);
                HttpContext.Session.SetString("UserName", user.FullName);

                // Redirect to patient dashboard
                return RedirectToAction("Dashboard", "PatientAccount");
            }

            ViewBag.Error = "❌ Invalid email or password. Please try again.";
            return View();
        }

        // ============================================================
        // REGISTER (GET)
        // ============================================================
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // ============================================================
        // REGISTER (POST)
        // ============================================================
        [HttpPost]
        public IActionResult Register(string FullName, string Email, string PasswordHash)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == Email);
            if (existingUser != null)
            {
                ViewBag.Message = "⚠️ This email is already registered.";
                return View();
            }

            var newUser = new User
            {
                FullName = FullName,
                Email = Email,
                PasswordHash = PasswordHash,
                Role = "Patient",
                CreatedAt = DateTime.Now,
                IsActive = true
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            ViewBag.Message = "✅ Registration successful! You can now log in.";
            return View();
        }

        // ============================================================
        // LOGOUT
        // ============================================================
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}
