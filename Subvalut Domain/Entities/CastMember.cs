using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Subvault_Domain.Entities {

    [Table("CastMembers")]
    public class CastMember : Person {

        //Many-to-many relation to Item
        public virtual ICollection<ItemCastMember> ItemCastMembers { get; set; }
    }
}
