using SQLite;
using System.ComponentModel.DataAnnotations.Schema;

namespace Academy.CodingTracker.Models;

public class CodingDay
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Unique]
    public DateTime Date { get; set; }
    public TimeSpan Duration { get; set; }
}
