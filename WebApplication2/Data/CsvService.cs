using GrapheneTrace.Models;
using System.Globalization;

namespace GrapheneTrace.Data
{
    public class CsvService
    {
        public List<SensorRecord> LoadSensorCsv(string filePath)
        {
            var records = new List<SensorRecord>();

            if (!File.Exists(filePath))
                return records;

            var lines = File.ReadAllLines(filePath);

            int index = 0;

            foreach (var rawLine in lines)
            {
                if (string.IsNullOrWhiteSpace(rawLine))
                    continue;

                string[] parts;

                if (rawLine.Contains(","))
                    parts = rawLine.Split(',');
                else
                    parts = rawLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length < 15)
                    continue;

                var values = parts
                    .Select(x => double.TryParse(x, NumberStyles.Any, CultureInfo.InvariantCulture, out double v) ? v : 0)
                    .ToList();

                double electrode014 = values[14];
                double avg = values.Average();
                double max = values.Max();

                records.Add(new SensorRecord
                {
                    Index = index++,
                    Electrode014 = electrode014,
                    AverageIntensity = avg,
                    PeakIntensity = max
                });
            }

            return records;
        }
    }
}
