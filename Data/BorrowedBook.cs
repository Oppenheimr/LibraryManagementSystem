namespace LibraryManagementSystem.Data;

public class BorrowedBook
{
    // Properties of the Book class
    public string Borrower { get; set; }
    public string ISBN { get; set; }
    public DateTimeOffset DeliveryDate { get; set; }
    

    // Constructors for the Book class
    
    /// <summary>
    /// For json serialize
    /// </summary>
    public BorrowedBook()
    {
        
    }
    
    public BorrowedBook(string isbn, string borrower)
    {
        ISBN = isbn;
        Borrower = borrower;
        DeliveryDate = DateTimeOffset.UtcNow + TimeSpan.FromDays(30);
    }
    
    public BorrowedBook(string isbn, string borrower, DateTimeOffset deliveryDate)
    {
        ISBN = isbn;
        Borrower = borrower;
        DeliveryDate = deliveryDate;
    }
}