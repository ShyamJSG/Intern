using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LibraryManagementWebApp.Models;

namespace LibraryManagementWebApp.Data
{
    public class LibraryManagementWebAppContext : DbContext
    {
        public LibraryManagementWebAppContext (DbContextOptions<LibraryManagementWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<LibraryManagementWebApp.Models.Book> Book { get; set; } = default!;
    }
}
