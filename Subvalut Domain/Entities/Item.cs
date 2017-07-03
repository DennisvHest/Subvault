using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Subvault_Domain.Entities {

    public abstract class Item {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Title { get; set; }

        [JsonProperty("overview")]
        public string Description { get; set; }

        [JsonProperty("release_date")]
        public DateTime? ReleaseDate { get; set; }

        [JsonProperty("poster_path")]
        public string PosterURL { get; set; }

        [JsonProperty("backdrop_path")]
        public string BackdropURL { get; set; }

        //Many-to-many relation to Genre
        public virtual ICollection<Genre> Genres { get; set; }

        //Many-to-many relation to CastMember
        public virtual ICollection<ItemCastMember> ItemCastMembers { get; set; }

        //Many-to-many relation to CrewMember
        public virtual ICollection<ItemCrewMember> ItemCrewMembers { get; set; }
    }
}
