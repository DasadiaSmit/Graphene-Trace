using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;

namespace GrapheneTrace.Controllers
{
    public class PatientAccountController : Controller
    {
        private readonly string sensorPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\sensor_data.txt");

        [HttpGet]
        public IActionResult Dashboard(string email = "example@gmail.com")
        {
            if (!System.IO.File.Exists(sensorPath))
            {
                ViewBag.Error = "Sensor data file not found!";
                return View();
            }

            var line = System.IO.File.ReadAllLines(sensorPath)
                                     .FirstOrDefault(l => l.StartsWith(email + ","));

            if (line == null)
            {
                ViewBag.Error = "No sensor data available for this user.";
                return View();
            }

            var parts = line.Split(',');

            ViewBag.Email = parts.ElementAtOrDefault(0);
            ViewBag.Temperature = parts.ElementAtOrDefault(1);
            ViewBag.HeartRate = parts.ElementAtOrDefault(2);
            ViewBag.Oxygen = parts.ElementAtOrDefault(3);

            return View();
        }
    }
}
