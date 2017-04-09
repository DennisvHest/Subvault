using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subvault_Domain.Entities {

    public class Movie : Item {

        //Foreign key to Subtitles
        public virtual ICollection<Subtitles> Subtitles { get; set; }
    }
}
