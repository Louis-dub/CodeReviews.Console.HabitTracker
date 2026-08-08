using HabitLoggerLibrary;

namespace HabitLoggerProgram;

class Program
{
    static void Main()
    {
        HabitLogger.CreateConnection();
        HabitLogger.AddOccurrence(3, "2026-08-08");
    }
}