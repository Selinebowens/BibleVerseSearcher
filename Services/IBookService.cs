using BibleVerseSearcher.Models;

namespace BibleVerseSearcher.Services
{
    /// <summary>
    /// Interface for Book business logic
    /// </summary>
    public interface IBookService
    {
        // Get all books for dropdown
        List<Book> GetAllBooks();

        // Get a specific book
        Book GetBookByNumber(int bookNumber);

        // Get Old Testament books only
        List<Book> GetOldTestamentBooks();

        // Get New Testament books only
        List<Book> GetNewTestamentBooks();
    }
}
