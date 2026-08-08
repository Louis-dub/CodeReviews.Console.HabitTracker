using Microsoft.Data.Sqlite;
using System;

namespace HabitLoggerLibrary;

public class HabitLogger
{
    private static void ExecuteQuery(string query)
    {
        using var connection = new SqliteConnection("Data Source=habitlogger.db");
        SqliteCommand sqlCommand = new(query, connection);
    
        try
        {
            connection.Open();
            sqlCommand.ExecuteNonQuery();
            Console.WriteLine("DataBase is Created Successfully");
        } catch (SqliteException e)
        {
            Console.WriteLine($"Sqlite Error: {e.Message}");
        }
    }
    public static void CreateConnection()
    {
        var createDB = """
            CREATE TABLE IF NOT EXISTS HabitLog
            (
                id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                quantity INT NOT NULL,
                date TEXT NOT NULL
            )
        """;
        ExecuteQuery(createDB);
    }

}
