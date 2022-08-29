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

            if (totalType == "week")
            {
                totalTicks = codingDays
                .Where(x => ISOWeek.GetWeekOfYear(x.Date) == currentWeek && x.Date.Year == today.Year)
                .Sum(x => x.Duration.Ticks);
            } 
            else if (totalType == "month")
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
    }
}
