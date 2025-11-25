using BibleVerseSearcher.Controllers;
using BibleVerseSearcher.Models;
using BibleVerseSearcher.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace BibleVerseSearcher.Controllers
{
    /// <summary>
    /// Controller for browsing Bible by reference and viewing verse details
    /// Handles: Book/Chapter selection, Verse display, Comment functionality
    /// </summary>
    public class BibleController : Controller
    {
        // Service dependencies injected through constructor
        private readonly IBookService _bookService;
        private readonly IVerseService _verseService;
        private readonly ICommentService _commentService;

        public BibleController(IBookService bookService, IVerseService verseService, ICommentService commentService)
        {
            _bookService = bookService;
            _verseService = verseService;
            _commentService = commentService;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// GET: /Bible/Browse
        /// Display the browse page with book dropdown
        /// User selects book, then chapter, then sees verses
        /// </summary>
        public IActionResult Browse()
        {
            // Get all 66 books to populate dropdown
            List<Book> books = _bookService.GetAllBooks();

            // Pass books to view using ViewBag
            ViewBag.Books = books;

            return View();
        }

        /// <summary>
        /// GET: /Bible/GetChapters?bookNumber=1
        /// AJAX endpoint to get chapters for a selected book
        /// Called by JavaScript when user selects a book from dropdown
        /// Returns JSON data
        /// </summary>
        public IActionResult GetChapters(int bookNumber)
        {
            // Get list of available chapters for this book
            List<int> chapters = _verseService.GetChaptersByBook(bookNumber);

            // Return as JSON for JavaScript to use
            return Json(new { chapters = chapters });
        }

        /// <summary>
        /// Get all verses for a specific book and chapter
        /// Called after user selects both book and chapter
        /// </summary>
        public IActionResult GetVerses(int bookNumber, int chapter)
        {
            // Get verses from service
            List<Verse> verses = _verseService.GetVersesByBookAndChapter(bookNumber, chapter);

            // Get book info to display book name
            Book book = _bookService.GetBookByNumber(bookNumber);

            // Pass data to view
            ViewBag.BookName = book?.BookName ?? "Unknown";
            ViewBag.Chapter = chapter;

            return View("VerseList", verses);
        }

        /// <summary>
        /// Display a single verse with its comments
        /// User can read the verse and see/add comments
        /// </summary>
        public IActionResult VerseDetail(int bookNumber, int chapter, int verse)
        {
            // Get the specific verse
            Verse verseData = _verseService.GetSingleVerse(bookNumber, chapter, verse);

            // If verse not found, show error
            if (verseData == null)
            {
                return NotFound("Verse not found");
            }

            // Get all comments for this verse
            List<Comment> comments = _commentService.GetCommentsByVerse(bookNumber, chapter, verse);

            // Pass verse and comments to view
            ViewBag.Comments = comments;

            return View(verseData);
        }

        /// <summary>
        /// POST: /Bible/AddComment
        /// Add a new comment to a verse
        /// User submits comment form, this saves it to database
        /// </summary>
        [HttpPost]
        public IActionResult AddComment(int bookNumber, int chapter, int verse, string commentText)
        {
            // Validate comment text is not empty
            if (string.IsNullOrWhiteSpace(commentText))
            {
                // If empty, redirect back with error
                TempData["Error"] = "Comment cannot be empty";
                return RedirectToAction("VerseDetail", new { bookNumber, chapter, verse });
            }

            // Create Comment object
            Comment comment = new Comment
            {
                BookNumber = bookNumber,
                Chapter = chapter,
                VerseNumber = verse,
                CommentText = commentText
            };

            // Save comment through service
            bool success = _commentService.AddComment(comment);

            // Set success or error message
            if (success)
            {
                TempData["Success"] = "Comment added successfully!";
            }
            else
            {
                TempData["Error"] = "Failed to add comment";
            }

            // Redirect back to verse detail page
            return RedirectToAction("VerseDetail", new { bookNumber, chapter, verse });
        }

        /// <summary>
        /// POST: /Bible/DeleteComment
        /// Delete a comment by its ID
        /// </summary>
        [HttpPost]
        public IActionResult DeleteComment(int commentId, int bookNumber, int chapter, int verse)
        {
            // Delete comment through service
            bool success = _commentService.DeleteComment(commentId);

            // Set message
            if (success)
            {
                TempData["Success"] = "Comment deleted successfully!";
            }
            else
            {
                TempData["Error"] = "Failed to delete comment";
            }

            // Redirect back to verse detail page
            return RedirectToAction("VerseDetail", new { bookNumber, chapter, verse });
        }
    }
}