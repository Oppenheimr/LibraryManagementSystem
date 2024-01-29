namespace LibraryManagementSystem.UI.Menu;

public class AddNewBookMenu : BaseMenu<AddNewBookMenu>
{
    public override void Display()
    {
        UserInterface.DisplayMenuHeader("Add a New Book");
        LibraryManager.Instance?.AddNewBook();
    }
}