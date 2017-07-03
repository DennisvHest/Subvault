using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Subvault_Domain.Entities {

    public class ItemCrewMember {

        [Key, Column(Order = 0)]
        public int ItemId { get; set; }
        [Key, Column(Order = 1)]
        public int CrewMemberId { get; set; }

        public virtual Item Item { get; set; }
        public virtual CrewMember CrewMember { get; set; }

        [Key, Column(Order = 2)]
        public string Job { get; set; }
    }
}
