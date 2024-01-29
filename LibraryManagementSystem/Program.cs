// See https://aka.ms/new-console-template for more information

using LibraryManagementSystem;
using LibraryManagementSystem.UI;
using LibraryManagementSystem.UI.Menu;

UserInterface.DisplayHeader("Welcome To Library Management System", ConsoleColor.DarkBlue);

Authenticator.Authenticate();

if (!Authenticator.IsGuest)
    UserInterface.DisplayHeader("Welcome To Library Management System " + Memory.UserName, ConsoleColor.DarkBlue);

while (Memory.ProgramLoop)
{
    try
    {
        MainMenu.Instance.Display();
    }
    catch (FormatException)
    {
        Memory.ProgramLoop = true;
        UserInterface.DisplayWarning("invalid character entry, make sure you entered the correct character.");
    }
    catch (Exception e)
    {
        UserInterface.DisplayWarning("An unexpected error was encountered. " +
                                     "Please report the error to the authorized person.");
        throw new Exception("Some unknown exception is occured.. " + e);
    }
}

Console.WriteLine("Close...");
await Task.Delay(4000);