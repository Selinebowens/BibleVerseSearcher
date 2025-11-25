using BibleVerseSearcher.Models;

namespace BibleVerseSearcher.DAOs
{
    /// <summary>
    /// Interface that defines database operations for Book table
    /// </summary>
    public interface IBookDAO
    {
        // Get all books from the database
        List<Book> GetAllBooks();

        // Get a specific book by its book number
        Book GetBookByNumber(int bookNumber);

        // Get all Old Testament books
        List<Book> GetOldTestamentBooks();

        // Get all New Testament books
        List<Book> GetNewTestamentBooks();
    }
}
