using LibraryManagementSystem.Data;
using LibraryManagementSystem.DataBase;

namespace LibraryManagementSystem.Extensions;

public static class BorrowedBookExtensions
{
    public static string Display(this BorrowedBook borrowed)
    {
        var book = borrowed.GetBook();
        return $"{book.Title}, {book.Author}, Borrower : {borrowed.Borrower}, ISBN : {book.ISBN}, Number of copies : {book.NumberOfCopies}, Copies on loan : {book.CopiesOnLoan}";
    }
    
    public static Book? GetBook(this BorrowedBook borrowed)
    {
        if (BookDataBase.Instance.Objects != null)
            return BookDataBase.Instance.Objects.First
                (book => book.Display().Contains(borrowed.ISBN, StringComparison.OrdinalIgnoreCase));
        return null;
    }
    
    public static int GetNumberOfDaysLate(this BorrowedBook borrowed) => (DateTimeOffset.UtcNow - borrowed.DeliveryDate).Days;
    
    public static Book?[] GetBooks(this BorrowedBook[]? borroweds) => borroweds.Select(borrowed => borrowed.GetBook()).ToArray();

    public static BorrowedBook[]? Search(this BorrowedBook[]? books, string keyword) =>
        books.Where(book => book.Display().Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToArray();
    
    public static BorrowedBook[]? SearchByTitle(this BorrowedBook[]? books, string keyword) =>
        books.Where(book => book.GetBook()!.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToArray();
    
    public static BorrowedBook[]? SearchByAuthor(this BorrowedBook[]? books, string keyword) =>
        books.Where(book => book.GetBook()!.Author.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToArray();
    
    public static BorrowedBook[]? SearchByBorrower(this BorrowedBook[]? books, string keyword) =>
        books.Where(book => book.Borrower.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToArray();
    
    public static BorrowedBook[]? SearchByISBN(this BorrowedBook[]? books, string keyword) =>
        books.Where(book => book.ISBN.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToArray();

    public static BorrowedBook[]? SearchExpired(this BorrowedBook[]? books) =>
        books.Where(book => DateTimeOffset.UtcNow > book.DeliveryDate).ToArray();

    public static BorrowedBook[]? SearchExpiredByBorrower(this BorrowedBook[]? books, string keyword) =>
        books.SearchByBorrower(keyword).SearchExpired();
}