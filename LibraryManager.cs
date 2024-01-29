using LibraryManagementSystem.Data;
using LibraryManagementSystem.DataBase;
using LibraryManagementSystem.Extensions;
using LibraryManagementSystem.UI;

namespace LibraryManagementSystem;

public class LibraryManager : SingletonBase<LibraryManager>
{
    public static Book[]? Books => BookDataBase.Instance.Objects;
    
    public Book[]? SearchBooks()
    {
        bool backToMainMenu = false;
        string keyword = "";
        Book[]? matchedBooks = null;
        MultipleGenericSelection.MultipleChoose(
            new Selection("General Searching", () =>
            {
                UserInterface.RequestMessageToUser("Key in the keywords you want to search.");
                keyword = UserInterface.ReadTheString();
                matchedBooks = BookDataBase.Instance.Objects.Search(keyword);
            }),
            new Selection("Searching for books by title", () =>
            {
                UserInterface.RequestMessageToUser("Key in the keywords you want to search.");
                keyword = UserInterface.ReadTheString();
                matchedBooks = BookDataBase.Instance.Objects.SearchByTitle(keyword);
            }),
            new Selection("Searching for books by author", () =>
            {
                UserInterface.RequestMessageToUser("Key in the keywords you want to search.");
                keyword = UserInterface.ReadTheString();
                matchedBooks = BookDataBase.Instance.Objects.SearchByAuthor(keyword);
            }),
            new Selection("Searching for books by ISBN", () =>
            {
                UserInterface.RequestMessageToUser("Key in the keywords you want to search.");
                keyword = UserInterface.ReadTheString();
                matchedBooks = BookDataBase.Instance.Objects.SearchByISBN(keyword);
            }), new Selection("Back to main menu", () => { backToMainMenu = true; }));

        if (backToMainMenu)
            return null;

        if (matchedBooks is { Length: > 0 })
        {
            Console.WriteLine($"Search Results for '{keyword}':");
            UserInterface.DisplayBookList(matchedBooks);
            return matchedBooks;
        }

        Console.WriteLine($"No books found for '{keyword}'.");
        return SearchBooks();
    }

    public Tuple<Book[]?, BorrowedBook[]?> SearchBorrowedBooks()
    {
        bool backToMainMenu = false;
        string keyword = "";
        Book[]? matchedBooks = null;
        BorrowedBook[]? matchedBorrowedBooks = null;
        MultipleGenericSelection.MultipleChoose(
            new Selection("Full List", () =>
            {
                matchedBorrowedBooks = BorrowedDataBase.Instance.Objects;
                matchedBooks = matchedBorrowedBooks.GetBooks()!;
            }),
            new Selection("General Searching", () =>
            {
                UserInterface.RequestMessageToUser("Key in the keywords you want to search.");
                keyword = UserInterface.ReadTheString();
                matchedBorrowedBooks = BorrowedDataBase.Instance.Objects.Search(keyword);
                matchedBooks = matchedBorrowedBooks.GetBooks()!;
            }),
            new Selection("Searching for books by title", () =>
            {
                UserInterface.RequestMessageToUser("Key in the keywords you want to search.");
                keyword = UserInterface.ReadTheString();
                matchedBorrowedBooks = BorrowedDataBase.Instance.Objects.SearchByTitle(keyword);
                matchedBooks = matchedBorrowedBooks.GetBooks()!;
            }),
            new Selection("Searching for books by author", () =>
            {
                UserInterface.RequestMessageToUser("Key in the keywords you want to search.");
                keyword = UserInterface.ReadTheString();
                matchedBorrowedBooks = BorrowedDataBase.Instance.Objects.SearchByAuthor(keyword);
                matchedBooks = matchedBorrowedBooks.GetBooks()!;
            }),
            new Selection("Searching for books by ISBN", () =>
            {
                UserInterface.RequestMessageToUser("Key in the keywords you want to search.");
                keyword = UserInterface.ReadTheString();
                matchedBorrowedBooks = BorrowedDataBase.Instance.Objects.SearchByISBN(keyword);
                matchedBooks = matchedBorrowedBooks.GetBooks()!;
            }), new Selection("Back to main menu", () => { backToMainMenu = true; }));

        if (backToMainMenu)
            return null;

        if (matchedBooks is { Length: > 0 })
        {
            Console.WriteLine($"Search Results for '{keyword}':");
            UserInterface.DisplayBorrowedList(matchedBorrowedBooks);
            return new Tuple<Book[]?, BorrowedBook[]?>(matchedBooks, matchedBorrowedBooks);
        }

        Console.WriteLine($"No books found for '{keyword}'.");
        return SearchBorrowedBooks();
    }

    public Tuple<Book[]?, BorrowedBook[]?> SearchBorrowedBooksByBorrower(string borrower)
    {
        bool backToMainMenu = false;
        Book[]? matchedBooks = null;
        BorrowedBook[]? matchedBorrowedBooks = null;
        
        matchedBorrowedBooks = BorrowedDataBase.Instance.Objects.SearchByBorrower(borrower);
        matchedBooks = matchedBorrowedBooks.GetBooks()!;
        
        if (backToMainMenu)
            return null;

        if (matchedBooks is { Length: > 0 })
        {
            Console.WriteLine($"List of books you've borrowed :");
            UserInterface.DisplayBorrowedList(matchedBorrowedBooks);
            return new Tuple<Book[]?, BorrowedBook[]?>(matchedBooks, matchedBorrowedBooks);
        }

        Console.WriteLine($"No books you borrowed were found. Your name (\"{borrower}\") was not found in the borrowed books");
        return null;
    }
    
    public Book[]? SearchExpiredBorrowedBooks()
    {
        bool backToMainMenu = false;
        string keyword = "";
        BorrowedBook[]? expiredBooks = null;
        var matchedBooks = new List<Book>();
        var matchedBooksInfos = new List<string>();
        
        MultipleGenericSelection.MultipleChoose(
            new Selection("All expired",
                () => { expiredBooks = BorrowedDataBase.Instance.Objects.SearchExpired(); }),
            new Selection("General Searching", () =>
            {
                UserInterface.RequestMessageToUser("Key in the keywords you want to search.");
                keyword = UserInterface.ReadTheString();
                expiredBooks = BorrowedDataBase.Instance.Objects.SearchExpired().Search(keyword);
            }),
            new Selection("Searching for books by title", () =>
            {
                UserInterface.RequestMessageToUser("Key in the keywords you want to search.");
                keyword = UserInterface.ReadTheString();
                expiredBooks = BorrowedDataBase.Instance.Objects.SearchExpired().SearchByTitle(keyword);
            }),
            new Selection("Searching for books by author", () =>
            {
                UserInterface.RequestMessageToUser("Key in the keywords you want to search.");
                keyword = UserInterface.ReadTheString();
                expiredBooks = BorrowedDataBase.Instance.Objects.SearchExpired().SearchByAuthor(keyword);
            }),
            new Selection("Searching for books by ISBN", () =>
            {
                UserInterface.RequestMessageToUser("Key in the keywords you want to search.");
                keyword = UserInterface.ReadTheString();
                expiredBooks = BorrowedDataBase.Instance.Objects.SearchExpired().SearchByISBN(keyword);
            }), new Selection("Back to main menu", () => { backToMainMenu = true; }));

        if (backToMainMenu)
            return null;

        if (expiredBooks == null) 
            return null;
                
        foreach (var expiredBook in expiredBooks)
        {
            var bookExtraInfos = expiredBook.GetBook();
            var displayInfo = bookExtraInfos.Display();
            displayInfo += $", {expiredBook.GetNumberOfDaysLate()} days late.";
            displayInfo += $", Borrower : {expiredBook.Borrower}.";
            matchedBooksInfos.Add(displayInfo);
            matchedBooks.Add(bookExtraInfos);
        }
        
        if (matchedBooks is { Count: > 0 })
        {
            Console.WriteLine($"Search Results for '{keyword}':");
            UserInterface.DisplayList(matchedBooksInfos.ToArray());
            return matchedBooks.ToArray();
        }

        Console.WriteLine($"No books found for '{keyword}'.");
        return SearchBorrowedBooks().Item1;
    }

    public BorrowedBook[]? SearchExpiredBorrowedBooksByUser()
    {
        var matchedBooks = BorrowedDataBase.Instance.Objects.SearchExpired().SearchByBorrower(Memory.UserName);

        if (matchedBooks is { Length: > 0 })
        {
            Console.WriteLine($"List of books you've borrowed :");
            UserInterface.DisplayBorrowedList(matchedBooks);
            return matchedBooks;
        }

        Console.WriteLine($"No books you borrowed were found. Your name (\"{Memory.UserName}\")" +
                          $" was not found in the borrowed books");
        return null;
    }

    public void AddNewBook()
    {
        UserInterface.RequestMessageToUser("Book Title: ");
        var name = UserInterface.ReadTheString();
        UserInterface.RequestMessageToUser("Book author: ");
        var author = UserInterface.ReadTheString();
        UserInterface.RequestMessageToUser("Book ISBN: ");
        var isbn = UserInterface.ReadTheString();
        UserInterface.RequestMessageToUser("Book number of copies: ");
        var copies = UserInterface.ReadTheInteger(1, int.MaxValue);

        var newBook = new Book(name, author, isbn, copies);

        if (UserInterface.GenericBinaryChoice($"You entered your book as : {newBook.Display()}." +
                                               $"Do you approve the book adding process?"))
            BookDataBase.Instance.AddObject(newBook);
    }

    public void BorrowBook(Book book)
    {
        if (string.IsNullOrEmpty(Memory.UserName))
        {
            UserInterface.DisplayWarning("Username is Invalid");
            return;
        }

        book.CopiesOnLoan++;
        BookDataBase.Instance.UpdateObject(book);

        //Yukarida zaten kontrol ettik.
#pragma warning disable CS8604 // Possible null reference argument.
        BorrowedDataBase.Instance.AddObject(new BorrowedBook(book.ISBN, Memory.UserName));
#pragma warning restore CS8604 // Possible null reference argument.
    }

    public void ReturnBook(Book book, string borrower)
    {
        if (string.IsNullOrEmpty(Memory.UserName))
        {
            UserInterface.DisplayWarning("Username is Invalid");
            return;
        }

        book.CopiesOnLoan--;
        BookDataBase.Instance.UpdateObject(book);

        //Yukarida zaten kontrol ettik.
#pragma warning disable CS8604 // Possible null reference argument.
        BorrowedDataBase.Instance.RemoveObject(book.Title, borrower);
#pragma warning restore CS8604 // Possible null reference argument.
    }
}