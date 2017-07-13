using Newtonsoft.Json;
using Subvault_Domain.Entities;
using System;
using System.Collections.Generic;

namespace Subvault_Domain.APIEntities {
    public class SeriesApiResult {

        public int Id { get; set; }
        public string Name { get; set; }

        [JsonProperty("overview")]
        public string Description { get; set; }

        [JsonProperty("air_date")]
        public DateTime? AirDate { get; set; }

        [JsonProperty("poster_path")]
        public string PosterURL { get; set; }

        [JsonProperty("backdrop_path")]
        public string BackdropPath { get; set; }

        [JsonProperty("number_of_seasons")]
        public int NumberOfSeasons { get; set; }

        //Many-to-many relation to Genre
        public virtual ICollection<Genre> Genres { get; set; }
    }
}
