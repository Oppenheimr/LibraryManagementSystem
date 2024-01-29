using LibraryManagementSystem.Data;

namespace LibraryManagementSystem.UI.Menu;

public class ReturnMenu : BaseMenu<ReturnMenu>
{
    public override void Display()
    {
        UserInterface.DisplayMenuHeader("Return a Book");
        
        while (true)
        {
            UserInterface.RequestMessageToUser("To select the book you want to return");

            Tuple<Book[]?, BorrowedBook[]?> books;
            int selectedIndex = 0;
            
            if (Memory.UserAccess == AccessLevel.Admin)
            {
                books = LibraryManager.Instance?.SearchBorrowedBooks();

                //if books is null, back to main menu
                if (books == null) return;
                while (books.Item1!.Length != 1)
                {
                    UserInterface.RequestMessageToUser(
                        $"If the book you are looking for is not available, press {books.Item1!.Length + 1} to search again.\n" +
                        $"Press {books.Item1!.Length + 2} to return to the main menu.\n" +
                        "If the book you are looking for is available, " +
                        "dial the number at the beginning of the line where the book's name is written.");

                    selectedIndex = UserInterface.ReadTheInteger(1, books.Item1!.Length + 1) - 1;

                    if (selectedIndex == books.Item1!.Length)
                    {
                        Display();
                        continue;
                    }

                    if (selectedIndex == books.Item1!.Length + 1)
                        return;

                    break;
                }
            }
            else
            {
                books = LibraryManager.Instance?.SearchBorrowedBooksByBorrower(Memory.UserName);

                //if books is null, back to main menu
                if (books == null) return;
                while (books.Item1!.Length != 1)
                {
                    UserInterface.RequestMessageToUser(
                        $"Press {books.Item1!.Length + 1} to return to the main menu.\n" +
                        "If the book you are looking for is available, " +
                        "dial the number at the beginning of the line where the book's name is written.");

                    selectedIndex = UserInterface.ReadTheInteger(1, books.Item1!.Length) - 1;

                    if (selectedIndex == books.Item1!.Length)
                        return;

                    break;
                }
            }

            var choice = UserInterface.GenericBinaryChoice(
                $"Do you approve of returning the book {books.Item1![selectedIndex].Title} ?");

            if (choice)
                LibraryManager.Instance?.ReturnBook(books.Item1![selectedIndex], books.Item2![selectedIndex].Borrower);

            break;
        }
    }
}