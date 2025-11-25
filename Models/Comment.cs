namespace BibleVerseSearcher.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int BookNumber { get; set; }
        public int Chapter { get; set; }
        public int VerseNumber { get; set; }
        public string CommentText { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
