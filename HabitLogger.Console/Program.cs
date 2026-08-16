using HabitLoggerProgram;

class Program
{
    static void Main()
    {
        AppDomain.CurrentDomain.ProcessExit += (s, e) => Console.CursorVisible = true;
        Console.CancelKeyPress += (s, e) => Console.CursorVisible = true;

        try
        {
            UserInterface.MainMenu();
        }
        finally
        {
            Console.CursorVisible = true;
        }
    }
}