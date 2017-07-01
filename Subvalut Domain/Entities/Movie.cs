using System.Collections.Generic;

namespace Subvault_Domain.Entities {

    public class Movie : Item {

        //Foreign key to Subtitles
        public virtual ICollection<Subtitles> Subtitles { get; set; }
    }
}
