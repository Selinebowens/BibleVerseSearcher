using BibleVerseSearcher.Models;

namespace BibleVerseSearcher.DAOs
{
    /// <summary>
    /// Interface that defines database operations for Verse table
    /// </summary>
    public interface IVerseDAO
    {
        // Get verses by book and chapter 
        List<Verse> GetVersesByBookAndChapter(int bookNumber, int chapter);

        // Get a single verse by book, chapter, and verse number
        Verse GetSingleVerse(int bookNumber, int chapter, int verseNumber);

        // Search verses by keyword 
        List<Verse> SearchVersesByKeyword(string keyword);

        // Search verses by keyword filtered by testament (OT or NT)
        List<Verse> SearchVersesByKeywordAndTestament(string keyword, string testament);

        // Get all chapters available for a specific book
        List<int> GetChaptersByBook(int bookNumber);
    }
}
