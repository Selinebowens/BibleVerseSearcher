using BibleVerseSearcher.DAOs;
using BibleVerseSearcher.Models;

namespace BibleVerseSearcher.Services
{
    /// <summary>
    /// Service class for Verse business logic
    /// Calls VerseDAO to interact with database
    /// Can add search logic, filtering, or formatting here
    /// </summary>
    public class VerseService : IVerseService
    {
        // Reference to VerseDAO for database operations
        private readonly IVerseDAO _verseDAO;

        /// <summary>
        /// Constructor: receives VerseDAO through dependency injection
        /// </summary>
        public VerseService(IVerseDAO verseDAO)
        {
            _verseDAO = verseDAO;
        }

        /// <summary>
        /// Get all verses for a specific book and chapter
        /// Controller calls this when user browses by reference
        /// </summary>
        public List<Verse> GetVersesByBookAndChapter(int bookNumber, int chapter)
        {
            // Could add validation here
            if (bookNumber < 1 || bookNumber > 66)
            {
                return new List<Verse>(); // Return empty list for invalid book
            }

            if (chapter < 1)
            {
                return new List<Verse>(); // Return empty list for invalid chapter
            }

            // Call DAO to get verses from database
            return _verseDAO.GetVersesByBookAndChapter(bookNumber, chapter);
        }

        /// <summary>
        /// Get a single specific verse
        /// Used for verse detail page
        /// </summary>
        public Verse GetSingleVerse(int bookNumber, int chapter, int verseNumber)
        {
            // Validate parameters
            if (bookNumber < 1 || bookNumber > 66 || chapter < 1 || verseNumber < 1)
            {
                return null; // Invalid parameters
            }

            // Call DAO to get verse from database
            return _verseDAO.GetSingleVerse(bookNumber, chapter, verseNumber);
        }

        /// <summary>
        /// Search all verses containing a keyword
        /// </summary>
        public List<Verse> SearchVersesByKeyword(string keyword)
        {
            // Validate keyword is not empty
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return new List<Verse>(); // Return empty list if no keyword
            }

            // Trim whitespace from keyword
            keyword = keyword.Trim();

            // Call DAO to search database
            return _verseDAO.SearchVersesByKeyword(keyword);
        }

        /// <summary>
        /// Search verses with testament filter (OT or NT)
        /// </summary>
        public List<Verse> SearchVersesByKeywordAndTestament(string keyword, string testament)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return new List<Verse>();
            }

            // Validate testament is either "OT" or "NT"
            if (testament != "OT" && testament != "NT")
            {
                return new List<Verse>();
            }

            // Trim keyword
            keyword = keyword.Trim();

            // Call DAO to search database with filter
            return _verseDAO.SearchVersesByKeywordAndTestament(keyword, testament);
        }

        /// <summary>
        /// Get list of chapters available for a book
        /// Used to populate chapter dropdown after book is selected
        /// </summary>
        public List<int> GetChaptersByBook(int bookNumber)
        {
            // Validate book number
            if (bookNumber < 1 || bookNumber > 66)
            {
                return new List<int>(); // Return empty list for invalid book
            }

            // Call DAO to get chapters from database
            return _verseDAO.GetChaptersByBook(bookNumber);
        }
    }
}
