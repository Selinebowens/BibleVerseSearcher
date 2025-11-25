using BibleVerseSearcher.Models;
using Microsoft.Data.SqlClient;

namespace BibleVerseSearcher.DAOs
{
    /// <summary>
    /// Data Access Object for Comment table
    /// Handles all database operations for user comments on verses
    /// </summary>
    public class CommentDAO : ICommentDAO
    {
        private readonly string _connectionString;

        public CommentDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Get all comments for a specific verse
        /// Used to display comments on the verse detail page
        /// Returns comments ordered by newest first
        /// </summary>
        public List<Comment> GetCommentsByVerse(int bookNumber, int chapter, int verseNumber)
        {
            List<Comment> comments = new List<Comment>();

            // SQL query to get all comments for a specific verse
            // ORDER BY created_at DESC shows newest comments first
            string query = @"
                SELECT id, book_number, chapter, verse, comment_text, created_at
                FROM [dbo].[verse_comments]
                WHERE book_number = @BookNumber 
                  AND chapter = @Chapter 
                  AND verse = @VerseNumber
                ORDER BY created_at DESC";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                // Add parameters to prevent SQL injection
                command.Parameters.AddWithValue("@BookNumber", bookNumber);
                command.Parameters.AddWithValue("@Chapter", chapter);
                command.Parameters.AddWithValue("@VerseNumber", verseNumber);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Loop through each comment and create Comment objects
                while (reader.Read())
                {
                    Comment comment = new Comment
                    {
                        Id = reader.GetInt32(0),                  
                        BookNumber = reader.GetInt32(1),           
                        Chapter = reader.GetInt32(2),              
                        VerseNumber = reader.GetInt32(3),         
                        CommentText = reader.GetString(4),        
                        CreatedAt = reader.GetDateTime(5)         
                    };
                    comments.Add(comment);
                }

                reader.Close();
            }

            return comments;
        }

        /// <summary>
        /// Add a new comment to a verse
        /// Used when user submits a comment on the verse detail page
        /// Returns true if successful, false if failed
        /// </summary>
        public bool AddComment(Comment comment)
        {
            // SQL INSERT statement to add new comment
            // created_at uses GETDATE() to automatically set current timestamp
            string query = @"
                INSERT INTO [dbo].[verse_comments] 
                (book_number, chapter, verse, comment_text, created_at)
                VALUES 
                (@BookNumber, @Chapter, @VerseNumber, @CommentText, GETDATE())";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                // Add parameters with comment data
                command.Parameters.AddWithValue("@BookNumber", comment.BookNumber);
                command.Parameters.AddWithValue("@Chapter", comment.Chapter);
                command.Parameters.AddWithValue("@VerseNumber", comment.VerseNumber);
                command.Parameters.AddWithValue("@CommentText", comment.CommentText);

                try
                {
                    connection.Open();

                    // ExecuteNonQuery returns number of rows affected
                    int rowsAffected = command.ExecuteNonQuery();

                    // Return true if at least one row was inserted
                    return rowsAffected > 0;
                }
                catch (Exception)
                {
                    // If any error occurs, return false
                    return false;
                }
            }
        }

        /// <summary>
        /// Delete a comment by its ID
        /// Used if user wants to remove their comment
        /// Returns true if successful, false if failed
        /// </summary>
        public bool DeleteComment(int commentId)
        {
            // SQL DELETE statement to remove comment
            string query = "DELETE FROM [dbo].[verse_comments] WHERE id = @CommentId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CommentId", commentId);

                try
                {
                    connection.Open();

                    // ExecuteNonQuery returns number of rows affected
                    int rowsAffected = command.ExecuteNonQuery();

                    // Return true if at least one row was deleted
                    return rowsAffected > 0;
                }
                catch (Exception)
                {
                    // If any error occurs, return false
                    return false;
                }
            }
        }

        /// <summary>
        /// Get a single comment by its ID
        /// Used to retrieve comment details before deletion or editing
        /// </summary>
        public Comment GetCommentById(int commentId)
        {
            Comment comment = null;

            // SQL query to get one specific comment
            string query = @"
                SELECT id, book_number, chapter, verse, comment_text, created_at
                FROM [dbo].[verse_comments]
                WHERE id = @CommentId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CommentId", commentId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // If comment found, create Comment object
                if (reader.Read())
                {
                    comment = new Comment
                    {
                        Id = reader.GetInt32(0),
                        BookNumber = reader.GetInt32(1),
                        Chapter = reader.GetInt32(2),
                        VerseNumber = reader.GetInt32(3),
                        CommentText = reader.GetString(4),
                        CreatedAt = reader.GetDateTime(5)
                    };
                }

                reader.Close();
            }

            return comment;
        }
    }
}
