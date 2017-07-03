using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Subvault_Domain.Entities {

    [Table("Movies")]
    public class Movie : Item {

        //Foreign key to Subtitles
        public virtual ICollection<Subtitles> Subtitles { get; set; }
    }
}
