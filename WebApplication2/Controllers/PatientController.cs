using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GrapheneTrace.Controllers
{
    public class PatientController : Controller
    {
        public IActionResult Dashboard()
        {
            var csvPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "sensor.csv");

            var lines = System.IO.File.ReadAllLines(csvPath);

            List<List<double>> heatmapData = new List<List<double>>();

            foreach (var line in lines.Skip(1))  // Skip header
            {
                var parts = line.Split(',');

                var temp = Convert.ToDouble(parts[1]);
                var heart = Convert.ToDouble(parts[2]);
                var oxygen = Convert.ToDouble(parts[3]);

                heatmapData.Add(new List<double> { temp, heart, oxygen });
            }

            ViewBag.HeatmapData = heatmapData;

            return View();
        }
    }
}
