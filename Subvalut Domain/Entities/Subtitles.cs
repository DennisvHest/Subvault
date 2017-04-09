using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subvault_Domain.Entities {

    public class Subtitles {

        public int Id { get; set; }
        public string Language { get; set; }
        public string FilePath { get; set; }

        //Foreign key to Movie
        public virtual ICollection<Movie> Items { get; set; }
    }
}
