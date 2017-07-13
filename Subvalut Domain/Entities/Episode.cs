using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Subvault_Domain.Entities {

    public class Episode {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "episode_number")]
        public int EpisodeNumber { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }

        [JsonProperty(PropertyName = "still_path")]
        public string StillPath { get; set; }

        [JsonProperty(PropertyName = "air_date")]
        public DateTime? AirDate { get;set;}

        //One-to-many relation to Season
        public virtual Season Season { get; set; }

        //One-to-many relation to Subtitles
        public virtual ICollection<Subtitles> Subtitles { get; set; }
    }
}
