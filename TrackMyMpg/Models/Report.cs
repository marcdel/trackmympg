using System.Collections.Generic;
using System.Linq;

namespace TrackMyMpg.Models
{
    public class Report
    {
        public Report(List<Vehicle> vehicles)
        {
            var grouped = vehicles.GroupBy(vehicle => vehicle.MakeId);

            Makes = grouped.Select(makes => new MakeStatistics
            {
                MakeId = makes.First().Make.Id,
                Make = makes.First().Make.Name,
                Average = makes.Average(make => make.Mpg),
                Minimum = makes.Min(make => make.Mpg),
                Maximum = makes.Max(make => make.Mpg)
            });
        }

        public IEnumerable<MakeStatistics> Makes { get; set; }
    }
}