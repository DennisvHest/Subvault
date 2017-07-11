using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Subvault_Domain.Entities {

    public class ItemGenre {

        [Key, Column(Order = 0)]
        public int ItemId { get; set; }
        [Key, Column(Order = 1)]
        public int GenreId { get; set; }

        public virtual Item Item { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
