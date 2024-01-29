using LibraryManagementSystem.Data;

namespace LibraryManagementSystem.Extensions;

public static class BookExtensions
{
    public static string Display(this Book book) => 
        $"{book.Title}, {book.Author}, ISBN : {book.ISBN}, Number of copies : {book.NumberOfCopies}, Copies on loan : {book.CopiesOnLoan}";
    
    public static Book[]? Search(this Book[]? books, string keyword) =>
        books.Where(book => book.Display().Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToArray();
    
    public static Book[]? SearchByTitle(this Book[]? books, string keyword) =>
        books.Where(book => book.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToArray();
    
    public static Book[]? SearchByAuthor(this Book[]? books, string keyword) =>
        books.Where(book => book.Author.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToArray();
    
    public static Book[]? SearchByISBN(this Book[]? books, string keyword) =>
        books.Where(book => book.ISBN.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToArray();
}