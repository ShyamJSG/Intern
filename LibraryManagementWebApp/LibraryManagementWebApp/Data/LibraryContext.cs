using Microsoft.EntityFrameworkCore;
using LibraryManagementWebApp.Models;

namespace LibraryManagementWebApp.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }
        public DbSet<Book> Books{ get; set; }
    }
}
