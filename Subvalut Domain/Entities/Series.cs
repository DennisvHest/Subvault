using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Subvault_Domain.Entities {

    [Table("Series")]
    public class Series : Item {

        //One-to-many relation to Season
        public virtual ICollection<Season> Seasons { get; set; }
    }
}
