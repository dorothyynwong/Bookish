using Bookish.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookish
{
    public class BookishContext : DbContext
    {
        public BookishContext(DbContextOptions<BookishContext> options)
        : base(options)
        { }
        // Put all the tables you want in your database here
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            // This is the configuration used for connecting to the database
            optionsBuilder.UseNpgsql(@"Server=localhost;Port=5432;Database=bookish;User Id=bookish;Password=bookish;");
        }
    }
}