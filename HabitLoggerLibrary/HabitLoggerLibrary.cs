using Microsoft.Data.Sqlite;
using System;
using Spectre.Console;

namespace HabitLoggerLibrary;

public class HabitLoggerDataBase
{
    private static Dictionary<SqliteConnection, SqliteDataReader>? GetDatas(string query, Dictionary<string, object> values)
    {
        var connection = new SqliteConnection("Data Source=habitlogger.db");
        SqliteCommand sqlCommand = new(query, connection);

        try
        {
            connection.Open();
            foreach(var (col, val) in values)
            {
                sqlCommand.Parameters.AddWithValue(col, val);
            }
            var reader = sqlCommand.ExecuteReader();
            return new Dictionary<SqliteConnection, SqliteDataReader>(){{connection, reader}};
        }
        catch (SqliteException e)
        {
            AnsiConsole.MarkupLine($"[red]Sqlite Error: {e.Message}[/]");
        }
        return null;
    }
    private static void ExecuteQuery(string query, Dictionary<string, object> values)
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
            sqlCommand.ExecuteNonQuery();
        }
        catch (SqliteException e)
        {
            AnsiConsole.MarkupLine($"[red]Sqlite Error: {e.Message}[/]");
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
    
        ExecuteQuery(createDB, values);
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

        ExecuteQuery(insertOccurrence, values);
    }

    public static void DeleteOccurrence(int id)
    {
        var deleteOccurrence = """
            DELETE FROM HabitLog
            WHERE id = @id
        """;
        var values = new Dictionary<string, object>
        {
            { "@id", id }
        };

        ExecuteQuery(deleteOccurrence, values);
    }

    public static void UpdateOccurrence(int id, Enums.UpdateCol col, object newData)
    {
        var updateOccurrence = "";
        if (col == Enums.UpdateCol.Quantity)
            updateOccurrence = """
                UPDATE HabitLog
                SET quantity = @newData
                WHERE id = @id
            """;
        else
            updateOccurrence = """
                UPDATE HabitLog
                SET date = @newData
                WHERE id = @id
            """;
        var values = new Dictionary<string, object>
        {
            { "@id", id },
            { "@col", col },
            { "@newData", newData }
        };

        ExecuteQuery(updateOccurrence, values);
    }

    public static Dictionary<SqliteConnection, SqliteDataReader>? GetAllOccurrences()
    {
        var selectAllOccurrences = """
            SELECT * FROM HabitLog
        """;
        var values = new Dictionary<string ,object>{};

        return GetDatas(selectAllOccurrences, values);
    }
}
