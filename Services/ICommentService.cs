using BibleVerseSearcher.Models;

namespace BibleVerseSearcher.Services
{
    /// <summary>
    /// Interface for Comment business logic
    /// </summary>
    public interface ICommentService
    {
        // Get all comments for a verse
        List<Comment> GetCommentsByVerse(int bookNumber, int chapter, int verseNumber);

        // Add a new comment
        bool AddComment(Comment comment);

        // Delete a comment
        bool DeleteComment(int commentId);

        // Get a comment by ID
        Comment GetCommentById(int commentId);
    }
}
