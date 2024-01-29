namespace LibraryManagementSystem;

public class SingletonBase
{
}

/// <summary>
/// Türetilecek tim sınıflara "Ins" tanımı ekler
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonBase<T> : SingletonBase where T : new()
{
    private static T? _instance;
    public static T? Instance => _instance != null ? _instance : new T();
}