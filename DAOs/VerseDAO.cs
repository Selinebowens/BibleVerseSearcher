using BibleVerseSearcher.Models;
using Microsoft.Data.SqlClient;

namespace BibleVerseSearcher.DAOs
{
    /// <summary>
    /// Data Access Object for Verse table
    /// Handles all database operations for verses
    /// </summary>
    public class VerseDAO : IVerseDAO
    {
        private readonly string _connectionString;

        public VerseDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Get all verses for a specific book and chapter
        /// Used when user browses by reference 
        /// Returns verses in order
        /// </summary>
        public List<Verse> GetVersesByBookAndChapter(int bookNumber, int chapter)
        {
            List<Verse> verses = new List<Verse>();

            // SQL query joins verses with books to get book name
            // Uses parameters to prevent SQL injection
            string query = @"
                SELECT v.id, v.book_number, v.chapter, v.verse, v.text, b.book_name
                FROM [dbo].[verses] v
                JOIN [dbo].[books] b ON v.book_number = b.book_number
                WHERE v.book_number = @BookNumber AND v.chapter = @Chapter
                ORDER BY v.verse";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                // Add parameters to prevent SQL injection
                command.Parameters.AddWithValue("@BookNumber", bookNumber);
                command.Parameters.AddWithValue("@Chapter", chapter);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Loop through results and create Verse objects
                while (reader.Read())
                {
                    Verse verse = new Verse
                    {
                        Id = reader.GetInt32(0),              
                        BookNumber = reader.GetInt32(1),       
                        Chapter = reader.GetInt32(2),          
                        VerseNumber = reader.GetInt32(3),      
                        Text = reader.GetString(4),            
                        BookName = reader.GetString(5)         
                    };
                    verses.Add(verse);
                }

                reader.Close();
            }

            return verses;
        }

        /// <summary>
        /// Get a single specific verse
        /// Used to display verse detail page with comments
        /// </summary>
        public Verse GetSingleVerse(int bookNumber, int chapter, int verseNumber)
        {
            Verse verse = null;

            // Query to get one specific verse with book name
            string query = @"
                SELECT v.id, v.book_number, v.chapter, v.verse, v.text, b.book_name
                FROM [dbo].[verses] v
                JOIN [dbo].[books] b ON v.book_number = b.book_number
                WHERE v.book_number = @BookNumber 
                  AND v.chapter = @Chapter 
                  AND v.verse = @VerseNumber";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                // Add parameters
                command.Parameters.AddWithValue("@BookNumber", bookNumber);
                command.Parameters.AddWithValue("@Chapter", chapter);
                command.Parameters.AddWithValue("@VerseNumber", verseNumber);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // If verse found, create Verse object
                if (reader.Read())
                {
                    verse = new Verse
                    {
                        Id = reader.GetInt32(0),
                        BookNumber = reader.GetInt32(1),
                        Chapter = reader.GetInt32(2),
                        VerseNumber = reader.GetInt32(3),
                        Text = reader.GetString(4),
                        BookName = reader.GetString(5)
                    };
                }

                reader.Close();
            }

            return verse;
        }

        /// <summary>
        /// Search all verses that contain a keyword
        /// </summary>
        public List<Verse> SearchVersesByKeyword(string keyword)
        {
            List<Verse> verses = new List<Verse>();

            // Query searches for keyword in verse text
            // LIKE operator with % wildcards matches any text containing the keyword
            string query = @"
                SELECT v.id, v.book_number, v.chapter, v.verse, v.text, b.book_name
                FROM [dbo].[verses] v
                JOIN [dbo].[books] b ON v.book_number = b.book_number
                WHERE v.text LIKE @Keyword
                ORDER BY v.book_number, v.chapter, v.verse";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                // Add % wildcards around keyword for partial matching
                command.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Verse verse = new Verse
                    {
                        Id = reader.GetInt32(0),
                        BookNumber = reader.GetInt32(1),
                        Chapter = reader.GetInt32(2),
                        VerseNumber = reader.GetInt32(3),
                        Text = reader.GetString(4),
                        BookName = reader.GetString(5)
                    };
                    verses.Add(verse);
                }

                reader.Close();
            }

            return verses;
        }

        /// <summary>
        /// Search verses by keyword filtered by testament (Old or New)
        /// </summary>
        public List<Verse> SearchVersesByKeywordAndTestament(string keyword, string testament)
        {
            List<Verse> verses = new List<Verse>();

            // Query adds testament filter to search
            string query = @"
                SELECT v.id, v.book_number, v.chapter, v.verse, v.text, b.book_name
                FROM [dbo].[verses] v
                JOIN [dbo].[books] b ON v.book_number = b.book_number
                WHERE v.text LIKE @Keyword AND b.testament = @Testament
                ORDER BY v.book_number, v.chapter, v.verse";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                // Add both keyword and testament parameters
                command.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                command.Parameters.AddWithValue("@Testament", testament);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Verse verse = new Verse
                    {
                        Id = reader.GetInt32(0),
                        BookNumber = reader.GetInt32(1),
                        Chapter = reader.GetInt32(2),
                        VerseNumber = reader.GetInt32(3),
                        Text = reader.GetString(4),
                        BookName = reader.GetString(5)
                    };
                    verses.Add(verse);
                }

                reader.Close();
            }

            return verses;
        }

        /// <summary>
        /// Get list of all chapters available for a specific book
        /// Used to populate the chapter dropdown after user selects a book
        /// Returns distinct chapter numbers in order
        /// </summary>
        public List<int> GetChaptersByBook(int bookNumber)
        {
            List<int> chapters = new List<int>();

            // Query gets unique chapter numbers for the book
            // DISTINCT ensures no duplicate chapter numbers
            string query = @"
                SELECT DISTINCT chapter 
                FROM [dbo].[verses] 
                WHERE book_number = @BookNumber 
                ORDER BY chapter";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@BookNumber", bookNumber);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Add each chapter number to list
                while (reader.Read())
                {
                    chapters.Add(reader.GetInt32(0));
                }

                reader.Close();
            }

            return chapters;
        }
    }
}
