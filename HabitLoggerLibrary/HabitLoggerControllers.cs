using Spectre.Console;
using Microsoft.Data.Sqlite;

namespace HabitLoggerLibrary;

public class HabitLoggerControllers
{
    public static void AddHabit()
    {
        
    }

    public static void ViewHabit(SqliteDataReader reader)
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

    public static void UpdateHabit()
    {
        
    }

    public static void DeleteHabit()
    {
        
    }
}