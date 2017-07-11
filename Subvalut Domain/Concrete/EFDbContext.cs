using Subvault_Domain.Entities;
using System.Data.Entity;

namespace Subvault_Domain.Concrete {

    public class EFDbContext : DbContext {

        public DbSet<Item> Items { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Subtitles> Subtitles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CastMember> CastMembers { get; set; }
        public DbSet<CrewMember> CrewMembers { get; set; }
        public DbSet<ItemGenre> ItemGenres { get; set; }
        public DbSet<ItemCrewMember> ItemCrewMembers { get; set; }
        public DbSet<ItemCastMember> ItemCastMembers { get; set; }
    }
}
