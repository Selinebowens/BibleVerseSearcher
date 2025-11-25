# Bible Verse Searcher

ASP.NET Core MVC application for searching and browsing Bible verses with personal commenting functionality.

## 🎯 Features
- **Browse by Reference:** Select book and chapter from cascading dropdowns to view verses
- **Keyword Search:** Search all verses by keyword with optional Old/New Testament filtering
- **Verse Details:** Click any verse to view full text and add personal study notes
- **Comment System:** Add, view, and delete comments on individual verses
- **Professional UI:** Custom CSS styling with responsive design

## 🛠️ Technologies Used
- **Backend:** ASP.NET Core MVC
- **Database:** SQL Server (Docker container)
- **Data Access:** ADO.NET with parameterized queries
- **Architecture:** N-Layer (Models, DAOs, Services, Controllers, Views)
- **Frontend:** Razor Views, Bootstrap 5, JavaScript/AJAX
- **Version Control:** Git & GitHub

## 📊 Database Structure

### Tables:
1. **books** - All 66 books of the Bible
   - book_number (PK)
   - book_name
   - testament (OT/NT)

2. **verses** - Bible verse text
   - id (PK)
   - book_number (FK)
   - chapter
   - verse
   - text

3. **verse_comments** - User study notes
   - id (PK)
   - book_number (FK)
   - chapter
   - verse
   - comment_text
   - created_at

### Sample Data:
- Genesis 1:1-10 (Creation story)
- Psalms 23:1-6 (The Lord is my shepherd)
- John 3:1-5, 16 (Born again, God so loved the world)

## 🏗️ N-Layer Architecture
```
┌─────────────────────────────────────────┐
│         Presentation Layer              │
│  (Views: Browse, Search, VerseDetail)   │
└─────────────────────────────────────────┘
                   ↓
┌─────────────────────────────────────────┐
│         Controller Layer                │
│  (BibleController, SearchController)    │
└─────────────────────────────────────────┘
                   ↓
┌─────────────────────────────────────────┐
│       Business Logic Layer              │
│  (BookService, VerseService, etc.)      │
└─────────────────────────────────────────┘
                   ↓
┌─────────────────────────────────────────┐
│       Data Access Layer (DAO)           │
│  (BookDAO, VerseDAO, CommentDAO)        │
└─────────────────────────────────────────┘
                   ↓
┌─────────────────────────────────────────┐
│         Database Layer                  │
│  (SQL Server - BibleVerse DB)           │
└─────────────────────────────────────────┘
```

## 🚀 Setup Instructions

### Prerequisites:
- Visual Studio 2022
- .NET 6.0 or higher
- Docker Desktop (for SQL Server)

### Installation Steps:

1. **Clone the repository:**
```bash
   git clone https://github.com/Selinebowens/BibleVerseSearcher.git
   cd BibleVerseSearcher
```

2. **Start SQL Server container:**
```bash
   docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourPassword123!" -p 1433:1433 --name sql-server -d mcr.microsoft.com/mssql/server:2019-latest
```

3. **Update connection string:**
   - Create `appsettings.Development.json`
   - Add your SQL Server connection string

4. **Create database:**
   - Run the SQL scripts in `/Database` folder (if provided)
   - Or create tables manually using the database scripts

5. **Insert sample data:**
   - Execute INSERT statements for books and verses

6. **Run the application:**
```bash
   dotnet run
```
   Or press **F5** in Visual Studio

7. **Access the app:**
   - Open browser to `https://localhost:7298` (or the port shown in console)

## 📖 Usage

### Browse Bible:
1. Click "Browse Bible" in navigation
2. Select a book from dropdown
3. Select a chapter
4. Click "View Verses"
5. Click any verse reference to see details and add comments

### Search Verses:
1. Click "Search Verses" in navigation
2. Enter a keyword (e.g., "love", "faith")
3. Optionally filter by Old or New Testament
4. Click "Search"
5. Results show all matching verses

## 🎓 Academic Context

**Course:** CST-350 - Enterprise Application Development  
**Assignment:** Activity 8 - Bible Verse Searcher  
**Institution:** Grand Canyon University  
**Semester:** Fall 2025

### Key Learning Objectives Demonstrated:
- N-Layer architecture design and implementation
- DAO pattern for data access
- Dependency injection in ASP.NET Core
- RESTful routing and MVC patterns
- Database design with relationships
- AJAX for dynamic UI updates
- Professional UI/UX design

## 👤 Author

**Seline Bowens**  
Student - Computer Science  
Grand Canyon University

## 📝 Notes

- This application uses sample verses for demonstration purposes
- The complete Bible (31,102 verses) can be loaded by importing the full t_kjv.sql file
- Connection strings should be stored in `appsettings.Development.json` (not tracked by Git)

## 📜 License

This is a student project created for educational purposes at Grand Canyon University.