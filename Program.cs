using BibleVerseSearcher.DAOs;
using BibleVerseSearcher.Services;

namespace BibleVerseSearcher
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            string connectionString = builder.Configuration.GetConnectionString("BibleVerseDB");
            builder.Services.AddSingleton<IBookDAO>(provider => new BookDAO(connectionString));
            builder.Services.AddSingleton<IVerseDAO>(provider => new VerseDAO(connectionString));
            builder.Services.AddSingleton<ICommentDAO>(provider => new CommentDAO(connectionString));
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IVerseService, VerseService>();
            builder.Services.AddScoped<ICommentService, CommentService>();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
