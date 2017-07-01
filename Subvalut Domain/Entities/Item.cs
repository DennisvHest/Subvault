using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Subvault_Domain.Entities {

    public class Item {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string PosterURL { get; set; }
        public string BackdropURL { get; set; }

        //Many-to-many relation to Genre
        public virtual ICollection<Genre> Genres { get; set; }

        //Many-to-many relation to Person
        public virtual ICollection<Person> People { get; set; }
    }
}
