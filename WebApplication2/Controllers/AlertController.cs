using GrapheneTrace.Data;
using GrapheneTrace.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace GrapheneTrace.Controllers
{
    public class AlertController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlertController(ApplicationDbContext context)
        {
            _context = context;
        }

        // AUTO SENSOR-BASED ALERT
        [HttpGet]
        public IActionResult CheckAlert()
        {
            var latest = _context.Metrics
                .OrderByDescending(m => m.CalculatedAt)
                .FirstOrDefault();

            if (latest == null)
                return Json(new { alert = false });

            int threshold = 50;

            if (latest.PeakPressure > threshold)
            {
                string msg = $"High Pressure Detected: {latest.PeakPressure}";

                bool exists = _context.Alerts.Any(a =>
                    a.Value == latest.PeakPressure &&
                    a.AlertType == "System" &&
                    a.Timestamp.Date == DateTime.Now.Date
                );

                if (!exists)
                {
                    _context.Alerts.Add(new Alert
                    {
                        Value = latest.PeakPressure,
                        Message = msg,
                        AlertType = "System",
                        Timestamp = DateTime.Now
                    });

                    _context.SaveChanges();
                }

                return Json(new { alert = true, message = msg });
            }

            return Json(new { alert = false });
        }

        // EMERGENCY MANUAL ALERT BY PATIENT
        [HttpPost]
        public IActionResult Emergency()
        {
            _context.Alerts.Add(new Alert
            {
                Value = 0,
                Message = "Patient triggered an Emergency Alert!",
                AlertType = "Emergency",
                Timestamp = DateTime.Now
            });

            _context.SaveChanges();

            return Json(new { success = true, message = "Emergency alert sent to clinician!" });
        }

        public IActionResult List()
        {
            var alerts = _context.Alerts
                .OrderByDescending(a => a.Timestamp)
                .ToList();

            return View(alerts);
        }
    }
}
