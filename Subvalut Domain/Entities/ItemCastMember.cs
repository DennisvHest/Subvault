using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Subvault_Domain.Entities {

    public class ItemCastMember {

        [Key, Column(Order = 0)]
        public int ItemId { get; set; }
        [Key, Column(Order = 1)]
        public int CastMemberId { get; set; }

        public virtual Item Item { get; set; }
        public virtual CastMember CastMember { get; set; }

        [Key, Column(Order = 2)]
        public string Character { get; set; }
    }
}
