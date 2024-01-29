using LibraryManagementSystem.Data;

namespace LibraryManagementSystem.UI.Menu;

public class MainMenu : BaseMenu<MainMenu>
{
    private static readonly Selection AddNewBook = new("Add a new book", AddNewBookMenu.Instance.Display);

    private static readonly Selection DisplayAllBooks = new("Display all books", () =>
    {
        UserInterface.DisplayMenuHeader("Book List");
        UserInterface.DisplayAllBooks();
    });

    private static readonly Selection SearchBook = new("Search books", () =>
    {
        UserInterface.DisplayMenuHeader("Search Books");
        LibraryManager.Instance?.SearchBooks();
    });

    private static readonly Selection BorrowBook = new("Borrow a book", BorrowMenu.Instance.Display);

    private static readonly Selection ReturnBook = new("Return a book", ReturnMenu.Instance.Display);

    private static readonly Selection ExpiredBooks = new("Expired books", ExpiredMenu.Instance.Display);

    private static readonly Selection Exit = new("Exit", null);
    
    
    public override void Display()
    {
        UserInterface.DisplayMenuHeader("Main Menu");

        var selections = new List<Selection>();
        
        if (Authenticator.IsAdmin)
            selections.Add(AddNewBook);
        
        selections.Add(DisplayAllBooks);
        selections.Add(SearchBook);

        if (!Authenticator.IsGuest)
        {
            selections.Add(BorrowBook);
            selections.Add(ReturnBook);
            selections.Add(ExpiredBooks);
        }

        selections.Add(Exit);
        
        Memory.ProgramLoop = MultipleGenericSelection.MultipleChoose(selections.ToArray()) != selections.Count - 1;
    }
}