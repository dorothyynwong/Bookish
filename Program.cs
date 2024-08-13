using Bookish.Models;
using Bookish;
using Microsoft.EntityFrameworkCore;

// using (var context = new BookishContext())
// {
//     context.Database.EnsureCreated();

//     var author1 = new Author() {FirstName = "John", Surname = "Smith"};
    
//     context.Authors.Add(author1);

//     context.SaveChanges();

//     foreach (var a in context.Authors) {
//         Console.WriteLine($"First Name: {a.FirstName}, Surname: {a.Surname}");
//     }
// }


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BookishContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
    // options.UseNpgsql(builder.Configuration.GetConnectionString("BookishContext")));
    // options.UseNpgsql(@"Server=localhost;Port=5432;Database=bookish;User Id=bookish;Password=bookish;"));
    // options.UseSqlite(builder.Configuration.GetConnectionString("MvcMovieContext")));

// Add services to the container.
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
