using Academy.CodingTracker.Models;
using System.Globalization;

namespace Academy.CodingTracker.Data
{
    public class CodingService
    {
        public TimeSpan GetTotalTime(List<CodingDay> codingDays, string totalType)
        {
            var today = DateTime.Now;
            var currentWeek = ISOWeek.GetWeekOfYear(today);
            long totalTicks;

            if (totalType == "Week")
            {
                totalTicks = codingDays
                .Where(x => ISOWeek.GetWeekOfYear(x.Date) == currentWeek && x.Date.Year == today.Year)
                .Sum(x => x.Duration.Ticks);
            }
            else if (totalType == "Month")
            {
                totalTicks = codingDays
                .Where(x => x.Date.Month == today.Month && x.Date.Year == today.Year)
                .Sum(x => x.Duration.Ticks);
            }
            else
            {
                totalTicks = codingDays
                .Where(x => x.Date.Year == today.Year)
                .Sum(x => x.Duration.Ticks);
            }

            return new TimeSpan(totalTicks);
        }

        public List<CodingReport> GetCodingReport(List<CodingDay> codingDays, string reportType)
        {   
            if (reportType == "Weekly")
            {
                return codingDays
                .GroupBy(x => new { Week = ISOWeek.GetWeekOfYear(x.Date), Year = x.Date.Year })
                .Select(y => new CodingReport
                {
                    Date = $"{y.Key.Week} - {y.Key.Year}",
                    Duration = new TimeSpan(y.Sum(z => z.Duration.Ticks))
                })
                .ToList();
            }
            else if (reportType == "Monthly")
            {
                return codingDays
                .GroupBy(x => new { Month = x.Date.ToString("MMM"), Year = x.Date.Year })
                .Select(y => new CodingReport
                {
                    Date = $"{y.Key.Month} - {y.Key.Year}",
                    Duration = new TimeSpan(y.Sum(z => z.Duration.Ticks))
                })
                .ToList();
            } 
            else 
            {
                return codingDays
                .GroupBy(x => x.Date.Year )
                .Select(y => new CodingReport
                {
                    Date = y.Key.ToString(),
                    Duration = new TimeSpan(y.Sum(z => z.Duration.Ticks))
                })
                .ToList();
            }
        }
    }
}
