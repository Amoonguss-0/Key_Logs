using Microsoft.EntityFrameworkCore;
using KeyLogger2.Models;

namespace KeyLogger2.Data
{
    public class KeyLogContext : DbContext
    {
        public KeyLogContext(DbContextOptions<KeyLogContext> options) : base(options)
        {

        }
        public DbSet<Members> Member { get; set; }

        public DbSet<Passwords> Password { get; set; }

        public DbSet<Home> Homes { get; set; }
    }
}
