using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Subvault_Domain.Entities {

    [Table("CrewMembers")]
    public class CrewMember : Person {

        public virtual ICollection<ItemCrewMember> ItemCrewMembers { get; set; }
    }
}
