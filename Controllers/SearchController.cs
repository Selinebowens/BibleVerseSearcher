using BibleVerseSearcher.Models;
using BibleVerseSearcher.Services;
using Microsoft.AspNetCore.Mvc;

namespace BibleVerseSearcher.Controllers
{
    /// <summary>
    /// Controller for searching verses by keyword
    /// Handles: Search form display, Keyword search, Testament filtering
    /// </summary>
    public class SearchController : Controller
    {
        // Service dependencies
        private readonly IVerseService _verseService;
        private readonly IBookService _bookService;

        /// <summary>
        /// Constructor - receives services through dependency injection
        /// </summary>
        public SearchController(IVerseService verseService, IBookService bookService)
        {
            _verseService = verseService;
            _bookService = bookService;
        }

        /// <summary>
        /// GET: /Search/Index
        /// Display the search page with search form
        /// User enters keyword and optionally selects testament filter
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// GET: /Search/Results?keyword=love&testament=NT
        /// Search for verses containing the keyword
        /// Optional testament filter (OT, NT, or empty for both)
        /// </summary>
        public IActionResult Results(string keyword, string testament)
        {
            // Validate keyword is provided
            if (string.IsNullOrWhiteSpace(keyword))
            {
                ViewBag.Error = "Please enter a search keyword";
                return View("Index");
            }

            // Store search parameters to display on results page
            ViewBag.Keyword = keyword;
            ViewBag.Testament = testament;

            // Declare results list
            List<Verse> verses;

            // Search based on testament filter
            if (string.IsNullOrEmpty(testament))
            {
                // No filter - search all verses
                verses = _verseService.SearchVersesByKeyword(keyword);
            }
            else if (testament == "OT" || testament == "NT")
            {
                // Filter by Old or New Testament
                verses = _verseService.SearchVersesByKeywordAndTestament(keyword, testament);
            }
            else
            {
                // Invalid testament value
                ViewBag.Error = "Invalid testament filter";
                return View("Index");
            }

            // Pass results to view
            ViewBag.ResultCount = verses.Count;

            return View(verses);
        }
    }
}
