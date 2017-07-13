using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Subvault_Domain.Entities {

    public class Season {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "season_number")]
        public int SeasonNumber { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }

        [JsonProperty(PropertyName = "poster_path")]
        public string PosterPath { get; set; }

        [JsonProperty(PropertyName = "air_date")]
        public DateTime? AirDate { get; set; }

        //One-to-many relation to Series
        public virtual Series Series { get; set; }

        //One-to-many relation to Episode
        public virtual ICollection<Episode> Episodes { get; set; }
    }
}
