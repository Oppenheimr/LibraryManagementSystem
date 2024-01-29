using LibraryManagementSystem.Data;
using LibraryManagementSystem.DataBase;
using LibraryManagementSystem.Extensions;

namespace LibraryManagementSystem.UI;

public class UserInterface
{
    public static void DisplayAllBooks() => DisplayBookList(LibraryManager.Books);

    public static void DisplayList(string[]? items)
    {
        var bookList = "";

        for (int i = 0; i < items.Length; i++)
            bookList += $"{i + 1}) " + items[i] + "\n";

        Console.WriteLine(bookList);
    }
    
    public static void DisplayBookList(Book[]? books)
    {
        var bookList = "";

        for (int i = 0; i < books.Length; i++)
            bookList += $"{i + 1}) " + books[i].Display() + "\n";

        Console.WriteLine(bookList);
    }
    
    public static void DisplayBorrowedList(BorrowedBook[]? books)
    {
        var matchedBooksInfos = new List<string>();
        foreach (var book in books)
        {
            var bookExtraInfos = book.GetBook();
            var displayInfo = bookExtraInfos.Display();
            displayInfo += $", {book.GetNumberOfDaysLate()} days late.";
            displayInfo += $", Borrower : {book.Borrower}.";
            matchedBooksInfos.Add(displayInfo);
        }
        
        DisplayList(matchedBooksInfos.ToArray());
    }
    
    public static bool GenericBinaryChoice(string message, string acceptDescription = "Accept")
    {
        RequestMessageToUser(message);
        bool accept = false;

        MultipleGenericSelection.MultipleChoose(
            new Selection(acceptDescription, () => { accept = true; }),
            new Selection("Cancel", () => { accept = false; }));
        return accept;
    }

    public static string ReadTheString(string emptyWarningMessage = "Could not read any input text," +
                                                                    " make sure you entered the text and try again.")
    {
        while (true)
        {
            var result = Console.ReadLine();
            if (!string.IsNullOrEmpty(result)) return result;
            RequestMessageToUser(emptyWarningMessage);
        }
    }
    
    /// <summary>
    /// min 5 and max 15 : math ; 15 >= X >= 5  
    /// </summary>
    /// <param name="minValue">Include value</param>
    /// <param name="maxValue">Include value</param>
    /// <returns></returns>
    public static int ReadTheInteger(int minValue, int maxValue)
    {
        while (true)
        {
            var result = Console.ReadLine();
            if (int.TryParse(result, out int integerResult))
            {
                if (integerResult >= minValue && integerResult <= maxValue)
                    return integerResult;
            }
            RequestMessageToUser("Invalid character input, make sure you entered the correct character and try again");
        }
    }

    #region General Messages

    
    public static void RequestMessageToUser(string message)
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine(message + " :");
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    public static void DisplayMenuHeader(string menuName, ConsoleColor color = ConsoleColor.DarkCyan)
    {
        Console.ForegroundColor = color;
        Console.WriteLine($"\nLibrary Management System : {menuName}\n");
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    public static void DisplayHeader(string header, ConsoleColor color = ConsoleColor.DarkCyan)
    {
        Console.ForegroundColor = color;
        Console.WriteLine($"\n{header}\n");
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    public static void DisplayWarning(string cause)
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine($"\nWarning! {cause}");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Try again...");
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    #endregion
}