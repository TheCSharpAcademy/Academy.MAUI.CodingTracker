using Academy.CodingTracker.Models;
using SQLite;

namespace Academy.CodingTracker.Data;

public class CodingRepository
{
    string _dbPath;
    private SQLiteConnection conn;

    public CodingRepository(string dbPath)
    {
        _dbPath = dbPath;
        CreateTable();
    }

    internal void CreateTable()
    {
        try
        {
            conn = new SQLiteConnection(_dbPath);
            conn.CreateTable<CodingDay>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    internal void Add(CodingDay codingDay)
    {
        conn = new SQLiteConnection(_dbPath);
        conn.Insert(codingDay);
    }

    internal void Delete(int id)
    {
        conn = new SQLiteConnection(_dbPath);
        conn.Delete(new CodingDay { Id = id });
    }

    internal List<CodingDay> GetAll()
    {
        try
        {
            CreateTable();
            return conn.Table<CodingDay>().ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return new List<CodingDay>();
    }
}
