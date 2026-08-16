using Microsoft.Data.Sqlite;
using System;
using Spectre.Console;

namespace HabitLoggerLibrary;

public class HabitLogger
{
    private static void DisplayData(SqliteDataReader reader)
    {
        AnsiConsole.MarkupLine("Number of Fruit and Vegetables per Day");
        var table = new Table();

        table.Border(TableBorder.Rounded);
        table.AddColumn("[yellow]Id[/]");
        table.AddColumn("[yellow]Quantity[/]");
        table.AddColumn("[yellow]Date[/]");

        while (reader.Read())
            table.AddRow(
                reader.GetInt32(0).ToString(),
                $"[cyan]{reader.GetInt32(1)}[/]",
                $"[cyan]{reader.GetString(2)}[/]"
            );
        AnsiConsole.Write(table);
    }
    private static void ExecuteQuery(string query, Dictionary<string, object> values, Enums.QueryType type)
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
            if (type == Enums.QueryType.NonQuery)
            {
                sqlCommand.ExecuteNonQuery();
            }
            else
            {
                using var reader = sqlCommand.ExecuteReader();
                DisplayData(reader);
            }
        } catch (SqliteException e)
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
    
        ExecuteQuery(createDB, values, Enums.QueryType.NonQuery);
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

        ExecuteQuery(insertOccurrence, values, Enums.QueryType.NonQuery);
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

        ExecuteQuery(deleteOccurrence, values, Enums.QueryType.NonQuery);
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

        ExecuteQuery(updateOccurrence, values, Enums.QueryType.NonQuery);
    }

    public static void GetAllOccurrences()
    {
        var selectAllOccurrences = """
            SELECT * FROM HabitLog
        """;
        var values = new Dictionary<string ,object>{};

        ExecuteQuery(selectAllOccurrences, values, Enums.QueryType.Reader);
    }
}
