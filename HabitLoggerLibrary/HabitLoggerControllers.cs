using Spectre.Console;
using Microsoft.Data.Sqlite;

namespace HabitLoggerLibrary;

public class HabitLoggerControllers
{
    public static void AddHabit()
    {
        
    }

    public static void ViewHabit()
    {
        Dictionary<SqliteConnection, SqliteDataReader>? db = HabitLoggerDataBase.GetAllOccurrences();
        if (db == null)
        {
            return;
        }
        AnsiConsole.MarkupLine("Number of Fruit and Vegetables per Day");
        var table = new Table();

        table.Border(TableBorder.Rounded);
        table.AddColumn("[yellow]Id[/]");
        table.AddColumn("[yellow]Quantity[/]");
        table.AddColumn("[yellow]Date[/]");

        while (db.Values.ElementAt(0).Read())
            table.AddRow(
                db.Values.ElementAt(0).GetInt32(0).ToString(),
                $"[cyan]{db.Values.ElementAt(0).GetInt32(1)}[/]",
                $"[cyan]{db.Values.ElementAt(0).GetString(2)}[/]"
            );
        AnsiConsole.Write(table);
        db.Values.ElementAt(0).Close();
        db.Keys.ElementAt(0).Close();
    }

    public static void UpdateHabit()
    {
        
    }

    public static void DeleteHabit()
    {
        
    }
}