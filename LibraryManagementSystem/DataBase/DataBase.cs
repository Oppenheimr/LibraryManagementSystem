using LibraryManagementSystem.Data;
using Newtonsoft.Json;

namespace LibraryManagementSystem.DataBase;

public abstract class DataBase<T>
{
    protected T[]? _objects;
    public T[]? Objects => _objects is { Length: > 0 } ? 
        _objects :
        (_objects = LoadObjects());
    
    /// <summary>
    /// Id'si bilinen bir nesnenin veritabanında bulur.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="book"></param>
    /// <returns></returns>
    public abstract bool LoadObject(string id, out T? book);
    
    
    /// <summary>
    /// Lokal veri tabanında bir objeyi günceller
    /// </summary>
    /// <param name="objectToUpdate"></param>
    public abstract void UpdateObject(T objectToUpdate);
    
    /// <summary>
    /// Veritabanı için oluşturulması gereken .json dosyasını oluşturur
    /// </summary>
    protected abstract void CreateFile();
    
    /// <summary>
    /// .json formatlı dosyanın yolunu döndürür
    /// </summary>
    /// <returns></returns>
    protected abstract string FilePath();
    
    /// <summary>
    /// Tüm lokal veri tabanını çağırır
    /// </summary>
    /// <returns></returns>
    public T[]? LoadObjects()
    {
        if (!File.Exists(FilePath()))
            CreateFile();

        string json = File.ReadAllText(FilePath());
        T[]? booksData = JsonConvert.DeserializeObject<GenericCollection<T>>(json)?.Collection;
        return booksData ?? Array.Empty<T>();
    }
    
    /// <summary>
    /// Lokal veri tabanına obje ekler
    /// </summary>
    /// <param name="objectToAdd"></param>
    public void AddObject(T objectToAdd)
    {
        var objects = LoadObjects();

        if (objects == null) return;
        var data = new T[objects.Length + 1];
        Array.Copy(objects, data, objects.Length);
        data[objects.Length] = objectToAdd;

        SaveObjects(data);
        _objects = data;
    }
    
    /// <summary>
    /// Lokal veri tabanından bir objeyi kaldırır
    /// </summary>
    /// <param name="objectToRemove"></param>
    public void RemoveObject(T objectToRemove)
    {
        var objects = LoadObjects();

        if (objects == null || objects.Length == 0) return;

        var list = new List<T>(objects);
        list.Remove(objectToRemove);

        objects = list.ToArray();
        SaveObjects(objects);
        _objects = objects;
    }

    /// <summary>
    /// Tüm veritabanını kaydeder
    /// </summary>
    /// <param name="objectsToSave"></param>
    public void SaveObjects(T[]? objectsToSave)
    {
        string json = JsonConvert.SerializeObject(new GenericCollection<T>() { Collection = objectsToSave });
        File.WriteAllText(FilePath(), json);
        _objects = objectsToSave;
    }
}