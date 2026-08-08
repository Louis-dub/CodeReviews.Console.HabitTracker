using Microsoft.Data.Sqlite;
using System;

namespace HabitLoggerLibrary;

public class HabitLogger
{
    private enum QueryType
    {
        NonQuery,
        Reader
    }
    private static void ExecuteQuery(string query, Dictionary<string, object> values, QueryType type)
    {
        using var connection = new SqliteConnection("Data Source=habitlogger.db");
        SqliteCommand sqlCommand = new(query, connection);
    
        try
        {
            connection.Open();
            foreach (var (col, val) in values)
            {
                sqlCommand.Parameters.AddWithValue(col, val);
            }
            if (type == QueryType.NonQuery)
            {
                sqlCommand.ExecuteNonQuery();
                Console.WriteLine("Query Success");
            }
            else
            {
                using var reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                    Console.WriteLine($"{reader.GetInt32(0)} | {reader.GetInt32(1)} | {reader.GetString(2)}");
            }
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
        var values = new Dictionary<string, object>{};
    
        ExecuteQuery(createDB, values, QueryType.NonQuery);
    }

    public static void AddOccurrence(int quantity, string date)
    {
        var insertOccurrence = """
            INSERT INTO HabitLog (quantity, date)
            VALUES (@quantity, @date)
        """;
        var values = new Dictionary<string, object>
        {
            { "@quantity", quantity },
            { "@date", date}
        };

        ExecuteQuery(insertOccurrence, values, QueryType.NonQuery);
    }

    public static void GetAllOccurrences()
    {
        var selectAllOccurrences = """
            SELECT * FROM HabitLog
        """;
        var values = new Dictionary<string ,object>{};

        ExecuteQuery(selectAllOccurrences, values, QueryType.Reader);
    }
}
