using BibleVerseSearcher.DAOs;
using BibleVerseSearcher.Models;

namespace BibleVerseSearcher.Services
{
    /// <summary>
    /// Service class for Comment business logic
    /// Calls CommentDAO to interact with database
    /// </summary>
    public class CommentService : ICommentService
    {
        // Reference to CommentDAO for database operations
        private readonly ICommentDAO _commentDAO;

        /// <summary>
        /// Constructor to receive CommentDAO through dependency injection
        /// </summary>
        public CommentService(ICommentDAO commentDAO)
        {
            _commentDAO = commentDAO;
        }

        /// <summary>
        /// Get all comments for a specific verse
        /// Controller calls this to display comments on verse detail page
        /// </summary>
        public List<Comment> GetCommentsByVerse(int bookNumber, int chapter, int verseNumber)
        {
            // Validate parameters
            if (bookNumber < 1 || bookNumber > 66 || chapter < 1 || verseNumber < 1)
            {
                return new List<Comment>(); // Return empty list for invalid parameters
            }

            // Call DAO to get comments from database
            return _commentDAO.GetCommentsByVerse(bookNumber, chapter, verseNumber);
        }

        /// <summary>
        /// Add a new comment to a verse
        /// Validates comment before saving
        /// </summary>
        public bool AddComment(Comment comment)
        {
            // Validate comment is not null
            if (comment == null)
            {
                return false;
            }

            // Validate comment text is not empty
            if (string.IsNullOrWhiteSpace(comment.CommentText))
            {
                return false;
            }

            // Validate verse reference
            if (comment.BookNumber < 1 || comment.BookNumber > 66 ||
                comment.Chapter < 1 || comment.VerseNumber < 1)
            {
                return false;
            }

            // Trim whitespace from comment text
            comment.CommentText = comment.CommentText.Trim();

            // Call DAO to insert comment into database
            return _commentDAO.AddComment(comment);
        }

        /// <summary>
        /// Delete a comment by ID
        /// </summary>
        public bool DeleteComment(int commentId)
        {
            // Validate comment ID is positive
            if (commentId <= 0)
            {
                return false;
            }

            // Call DAO to delete comment from database
            return _commentDAO.DeleteComment(commentId);
        }

        /// <summary>
        /// Get a single comment by its ID
        /// Used to retrieve comment details
        /// </summary>
        public Comment GetCommentById(int commentId)
        {
            // Validate comment ID
            if (commentId <= 0)
            {
                return null;
            }

            // Call DAO to get comment from database
            return _commentDAO.GetCommentById(commentId);
        }
    }
}
