using Microsoft.EntityFrameworkCore;
using Udemy.Api.Models;

namespace Udemy.Api.Context
{
    public class UdemyContext : DbContext
    {
        public UdemyContext()
        { }

        public UdemyContext(DbContextOptions<UdemyContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
