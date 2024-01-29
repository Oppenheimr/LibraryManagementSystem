namespace LibraryManagementSystem.Data;

public class Book
{
    // Properties of the Book class
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public int NumberOfCopies { get; set; }
    public int CopiesOnLoan { get; set; }

    // Constructors for the Book class
    public Book(string title, string author, string isbn, int numberOfCopies)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        NumberOfCopies = numberOfCopies;
        CopiesOnLoan = 0; // Initially, no copies are on loan
    }
    
    // Constructor for the Book class
    public Book(string title, string author, string isbn, int numberOfCopies, int copiesOnLoan)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        NumberOfCopies = numberOfCopies;
        CopiesOnLoan = copiesOnLoan;
    }
    
    /// <summary>
    /// For json serialize
    /// </summary>
    public Book()
    {
        
    }
}