using LibraryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Models;

public class LibraryContext : DbContext
{
    public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }
    public DbSet<Books> Books { get; set; } = null!;
}