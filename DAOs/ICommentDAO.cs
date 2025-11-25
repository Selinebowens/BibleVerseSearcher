using BibleVerseSearcher.Models;

namespace BibleVerseSearcher.DAOs
{
    // <summary>
    /// Interface that defines database operations for Comment table
    /// </summary>
    public interface ICommentDAO
    {
        // Get all comments for a specific verse
        List<Comment> GetCommentsByVerse(int bookNumber, int chapter, int verseNumber);

        // Add a new comment to a verse
        bool AddComment(Comment comment);

        // Delete a comment by its ID
        bool DeleteComment(int commentId);

        // Get a single comment by ID
        Comment GetCommentById(int commentId);
    }
}
