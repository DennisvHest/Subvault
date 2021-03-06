﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Subvault_Domain.Entities {

    public class Genre {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }

        //Many-to-may relation to Item
        public virtual ICollection<ItemGenre> ItemGenres { get; set; }
    }
}
