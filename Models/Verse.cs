namespace BibleVerseSearcher.Models
{
    public class Verse
    {
        public int Id { get; set; }
        public int BookNumber { get; set; }
        public int Chapter { get; set; }
        public int VerseNumber { get; set; }
        public string Text { get; set; }

        // Navigation property
        public string BookName { get; set; }  // displays book name with verse
    }
}
