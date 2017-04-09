using Subvault_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subvault_Domain.Concrete {

    public class EFDbContext : DbContext {

        public DbSet<Item> Items { get; set; }
        public DbSet<Subtitles> Subtitles { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
