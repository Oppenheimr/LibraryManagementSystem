namespace LibraryManagementSystem.UI.Menu;

public class BorrowMenu : BaseMenu<BorrowMenu>
{
    public override void Display()
    {
        UserInterface.DisplayMenuHeader("Borrow a Book");
        
        while (true)
        {
            UserInterface.RequestMessageToUser("To select the book you want to borrow");
            var books = LibraryManager.Instance?.SearchBooks();

            //if books is null, back to main menu
            if (books == null) return;
            int selectedIndex = 0;
            while (books.Length != 1)
            {
                UserInterface.RequestMessageToUser(
                    $"If the book you are looking for is not available, press {books.Length + 1} to search again.\n" +
                    $"Press {books.Length + 2} to return to the main menu.\n" +
                    "If the book you are looking for is available, " +
                    "dial the number at the beginning of the line where the book's name is written.");

                selectedIndex = UserInterface.ReadTheInteger(1, books.Length + 1) - 1;

                if (selectedIndex == books.Length)
                {
                    Display();
                    continue;
                }

                if (selectedIndex == books.Length + 1)
                    return;
            }

            var choice = UserInterface.GenericBinaryChoice(
                $"Do you approve of borrowing the book {books[selectedIndex].Title} ?");

            if (choice)
                LibraryManager.Instance?.BorrowBook(books[selectedIndex]);

            break;
        }
    }
}