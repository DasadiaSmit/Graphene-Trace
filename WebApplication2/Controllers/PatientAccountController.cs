using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;

namespace WebApplication2.Controllers
{
    public class PatientAccountController : Controller
    {
        private readonly string dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\patients.txt");


        // GET: /PatientAccount/PatientProfile?email=user@email.com
        [HttpGet]
        public IActionResult PatientProfile(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ViewBag.Error = "Email parameter missing.";
                return View();
            }

            if (!System.IO.File.Exists(dataPath))
            {
                ViewBag.Error = "Patient data file not found!";
                return View();
            }

            var line = System.IO.File.ReadAllLines(dataPath)
                        .FirstOrDefault(l => l.StartsWith(email + ","));

            if (line == null)
            {
                ViewBag.Error = "Profile not found!";
                return View();
            }

            string[] parts = line.Split(',');
            ViewBag.Email = parts[0];
            ViewBag.Password = parts[1];
            ViewBag.Name = parts[2];
            ViewBag.Contact = parts[3];
            ViewBag.Address = parts[4];

            return View();
        }

        // POST: /PatientAccount/UpdateProfile
        [HttpPost]
        public IActionResult UpdateProfile(string Email, string Password, string Name, string Contact, string Address)
        {
            if (!System.IO.File.Exists(dataPath))
            {
                ViewBag.Error = "Patient data file not found!";
                return View("PatientProfile");
            }

            var allLines = System.IO.File.ReadAllLines(dataPath).ToList();

            for (int i = 0; i < allLines.Count; i++)
            {
                string[] parts = allLines[i].Split(',');
                if (parts[0] == Email)
                {
                    allLines[i] = $"{Email},{Password},{Name},{Contact},{Address}";
                    break;
                }
            }

            System.IO.File.WriteAllLines(dataPath, allLines);
            ViewBag.Message = "✅ Profile updated successfully!";
            return RedirectToAction("PatientProfile", new { email = Email });
        }
    }
}
