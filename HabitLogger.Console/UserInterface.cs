using HabitLoggerLibrary;
using Spectre.Console;

namespace HabitLoggerProgram;

internal class UserInterface
{
    static internal void MainMenu()
    {
        HabitLogger.CreateConnection();
        HabitLogger.GetAllOccurrences();
    }
}