using libApp.Models;
using System.Collections.Generic;
using System.Security;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace libApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<libApp.Models.Book>? Book { get; set; }
        public DbSet<libApp.Models.Genre>? Genre { get; set; }
        public DbSet<libApp.Models.Author>? Author { get; set; }
        public DbSet<libApp.Models.Customer>? Customer { get; set; }
        public DbSet<libApp.Models.BookCustomer>? BookCustomer { get; set; }
    }
}

