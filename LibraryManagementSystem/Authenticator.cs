using LibraryManagementSystem.Data;
using LibraryManagementSystem.UI;

namespace LibraryManagementSystem;

public static class Authenticator
{
    public static void Authenticate()
    {
        UserInterface.DisplayHeader("Authentication");
        UserInterface.RequestMessageToUser("Choose login method");
        Selection[] selections =
        {
            new("Login in as admin", null),
            new("Login in as user", null),
            new("Login in as Guest", null),
            new("Exit", null)
        };

        Memory.UserAccess = (AccessLevel)MultipleGenericSelection.MultipleChoose(selections);

        switch (Memory.UserAccess)
        {
            case AccessLevel.Admin or AccessLevel.User:

                string? userName = null;
                while (string.IsNullOrEmpty(userName))
                {
                    UserInterface.RequestMessageToUser("Please key in your name");
                    userName = UserInterface.ReadTheString();
                    
                    if (!UserInterface.GenericBinaryChoice($"You entered your name as {userName}." +
                                                           $" Do you approve the login?"))
                        userName = null;
                }

                Memory.UserName = userName;
                break;
            case AccessLevel.Guest:
                Memory.UserName = "Guest";
                break;
            case AccessLevel.Unconfirmed:
            default:
                Memory.ProgramLoop = false;
                break;
        }
    }

    public static bool IsAdmin => Memory.UserAccess == AccessLevel.Admin;
    public static bool IsUser => Memory.UserAccess == AccessLevel.User;
    public static bool IsGuest => Memory.UserAccess == AccessLevel.Guest;
}