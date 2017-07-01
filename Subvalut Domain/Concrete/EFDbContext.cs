using Subvault_Domain.Entities;
using System.Data.Entity;

namespace Subvault_Domain.Concrete {

    public class EFDbContext : DbContext {

        public DbSet<Item> Items { get; set; }
        public DbSet<Subtitles> Subtitles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Person> People { get; set; }
    }
}
