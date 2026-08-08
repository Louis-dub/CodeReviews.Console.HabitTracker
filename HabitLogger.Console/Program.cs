using HabitLoggerLibrary;

namespace HabitLoggerProgram;

class Program
{
    static void Main()
    {
        HabitLogger.CreateConnection();
        HabitLogger.AddOccurrence(3, "2026-08-08");
        HabitLogger.AddOccurrence(4, "2025-05-16");
        HabitLogger.AddOccurrence(5, "2025-05-16");
        HabitLogger.GetAllOccurrences();
        HabitLogger.DeleteOccurrence(1);
        HabitLogger.UpdateOccurrence(2, Enums.UpdateCol.Quantity, 10);
        HabitLogger.GetAllOccurrences();
        HabitLogger.DeleteOccurrence(5);
    }
}