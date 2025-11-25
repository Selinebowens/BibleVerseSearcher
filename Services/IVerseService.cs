using BibleVerseSearcher.Models;

namespace BibleVerseSearcher.Services
{
    /// <summary>
    /// Interface for Verse business logic
    /// </summary>
    public interface IVerseService
    {
        // Get verses by book and chapter for browsing
        List<Verse> GetVersesByBookAndChapter(int bookNumber, int chapter);

        // Get a single verse for detail page
        Verse GetSingleVerse(int bookNumber, int chapter, int verseNumber);

        // Search verses by keyword
        List<Verse> SearchVersesByKeyword(string keyword);

        // Search verses by keyword with testament filter
        List<Verse> SearchVersesByKeywordAndTestament(string keyword, string testament);

        // Get available chapters for a book
        List<int> GetChaptersByBook(int bookNumber);
    }
}
