using HabitLoggerLibrary;
using Spectre.Console;
using System;

namespace HabitLoggerProgram;

internal class UserInterface
{
    static internal void MainMenu()
    {
        HabitLoggerDataBase.CreateConnection();
        int launchApp = 0;

        while (launchApp == 0)
        {
            Dictionary<Enums.MenuAction, string> actions = new()
            {
                {Enums.MenuAction.AddHabit, "Add an Habit"},
                {Enums.MenuAction.ViewHabit, "View Habits"},
                {Enums.MenuAction.UpdateHabit, "Update an Habits"},
                {Enums.MenuAction.DeleteHabit, "Delete an Habits"},
                {Enums.MenuAction.Exit, "Exit"}
            };

            var actionChoice = AnsiConsole.Prompt(
                new SelectionPrompt<Enums.MenuAction>()
                .Title("What do you want to do next ?")
                .AddChoices(actions.Keys)
                .UseConverter(action => actions[action])
            );

            switch (actionChoice)
            {
                case Enums.MenuAction.Exit:
                    launchApp = 1;
                    break;
            }
        }
    }
}