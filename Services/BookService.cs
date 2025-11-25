using BibleVerseSearcher.DAOs;
using BibleVerseSearcher.Models;

namespace BibleVerseSearcher.Services
{
    /// <summary>
    /// Service class for Book business logic
    /// Calls BookDAO to interact with database
    /// </summary>
    public class BookService : IBookService
    {
        // Reference to BookDAO for database operations
        private readonly IBookDAO _bookDAO;

        /// <summary>
        /// Constructor: receives BookDAO through dependency injection
        /// </summary>
        public BookService(IBookDAO bookDAO)
        {
            _bookDAO = bookDAO;
        }

        /// <summary>
        /// Get all 66 books
        /// Controller calls this to populate book dropdown
        /// </summary>
        public List<Book> GetAllBooks()
        {
            // Call DAO to get books from database
            return _bookDAO.GetAllBooks();
        }

        /// <summary>
        /// Get a specific book by number
        /// Used to display book details
        /// </summary>
        public Book GetBookByNumber(int bookNumber)
        {
            if (bookNumber < 1 || bookNumber > 66)
            {
                return null; // Invalid book number
            }

            // Call DAO to get book from database
            return _bookDAO.GetBookByNumber(bookNumber);
        }

        /// <summary>
        /// Get only Old Testament books
        /// Used for testament filter in search
        /// </summary>
        public List<Book> GetOldTestamentBooks()
        {
            return _bookDAO.GetOldTestamentBooks();
        }

        /// <summary>
        /// Get only New Testament books
        /// Used for testament filter in search
        /// </summary>
        public List<Book> GetNewTestamentBooks()
        {
            return _bookDAO.GetNewTestamentBooks();
        }
    }
}
