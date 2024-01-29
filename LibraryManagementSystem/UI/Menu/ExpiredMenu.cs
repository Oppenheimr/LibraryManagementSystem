namespace LibraryManagementSystem.UI.Menu;

public class ExpiredMenu : BaseMenu<ExpiredMenu>
{
    public override void Display()
    {
        UserInterface.DisplayMenuHeader("Expired Books");
        
        if (Authenticator.IsAdmin)
            LibraryManager.Instance?.SearchExpiredBorrowedBooks();
        else
            LibraryManager.Instance?.SearchExpiredBorrowedBooksByUser();
    }
}