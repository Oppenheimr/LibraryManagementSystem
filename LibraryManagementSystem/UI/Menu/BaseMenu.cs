namespace LibraryManagementSystem.UI.Menu;

public abstract class BaseMenu<T> where T : new()
{
    public abstract void Display();

    private static T _instance;
    public static T Instance => _instance != null ? _instance : (_instance = new T());
}