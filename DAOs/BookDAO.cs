using BibleVerseSearcher.Models;
using Microsoft.Data.SqlClient;

namespace BibleVerseSearcher.DAOs
{
    /// <summary>
    /// Data Access Object for Book table
    /// Handles all database operations for books
    /// </summary>
    public class BookDAO : IBookDAO
    {
        private readonly string _connectionString;

        public BookDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Get all 66 books from the database
        /// Used to populate the book dropdown
        /// </summary>
        public List<Book> GetAllBooks()
        {
            // Create empty list to store books
            List<Book> books = new List<Book>();

            // SQL query to get all books ordered by book number
            string query = "SELECT book_number, book_name, testament FROM [dbo].[books] ORDER BY book_number";

            // Using statement ensures connection is closed after use
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // Create command with query and connection
                SqlCommand command = new SqlCommand(query, connection);

                // Open database connection
                connection.Open();

                // Execute query and get data reader
                SqlDataReader reader = command.ExecuteReader();

                // Loop through each row returned
                while (reader.Read())
                {
                    // Create a Book object for each row
                    Book book = new Book
                    {
                        BookNumber = reader.GetInt32(0),      // First column: book_number
                        BookName = reader.GetString(1),        // Second column: book_name
                        Testament = reader.GetString(2)        // Third column: testament
                    };

                    // Add book to the list
                    books.Add(book);
                }

                // Close reader
                reader.Close();
            }

            // Return the list of all books
            return books;
        }

        /// <summary>
        /// Get a specific book by its number
        /// </summary>
        public Book GetBookByNumber(int bookNumber)
        {
            Book book = null;

            // SQL query with parameter to prevent SQL injection
            string query = "SELECT book_number, book_name, testament FROM [dbo].[books] WHERE book_number = @BookNumber";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                // Add parameter to query
                command.Parameters.AddWithValue("@BookNumber", bookNumber);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // If book found, create Book object
                if (reader.Read())
                {
                    book = new Book
                    {
                        BookNumber = reader.GetInt32(0),
                        BookName = reader.GetString(1),
                        Testament = reader.GetString(2)
                    };
                }

                reader.Close();
            }

            return book;
        }

        /// <summary>
        /// Get all Old Testament books (testament = 'OT')
        /// </summary>
        public List<Book> GetOldTestamentBooks()
        {
            List<Book> books = new List<Book>();

            string query = "SELECT book_number, book_name, testament FROM [dbo].[books] WHERE testament = 'OT' ORDER BY book_number";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Book book = new Book
                    {
                        BookNumber = reader.GetInt32(0),
                        BookName = reader.GetString(1),
                        Testament = reader.GetString(2)
                    };
                    books.Add(book);
                }

                reader.Close();
            }

            return books;
        }

        /// <summary>
        /// Get all New Testament books (testament = 'NT')
        /// </summary>
        public List<Book> GetNewTestamentBooks()
        {
            List<Book> books = new List<Book>();

            string query = "SELECT book_number, book_name, testament FROM [dbo].[books] WHERE testament = 'NT' ORDER BY book_number";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Book book = new Book
                    {
                        BookNumber = reader.GetInt32(0),
                        BookName = reader.GetString(1),
                        Testament = reader.GetString(2)
                    };
                    books.Add(book);
                }

                reader.Close();
            }

            return books;
        }
    }
}

