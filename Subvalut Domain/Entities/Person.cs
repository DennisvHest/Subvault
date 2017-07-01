using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Subvault_Domain.Entities {

    public abstract class Person {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }

        //Many-to-many relation to Item
        public virtual ICollection<Item> Items { get; set; }
    }
}
