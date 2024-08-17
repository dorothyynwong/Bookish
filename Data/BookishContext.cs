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

        public DbSet<Member> Members {get; set;}

        public DbSet<Loan> Loans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Fluent API configurations (if any)
        }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // { 
        //     var configuration = new ConfigurationBuilder()
        //         .SetBasePath(Directory.GetCurrentDirectory())
        //         .AddJsonFile("appsettings.json")
        //         .Build();
        //     var connectionString = configuration.GetConnectionString("DefaultConnection");
        //     // This is the configuration used for connecting to the database
        //     optionsBuilder.UseNpgsql(connectionString);
            
        // }
    }
}