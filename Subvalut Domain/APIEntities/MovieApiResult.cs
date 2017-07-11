using Newtonsoft.Json;
using Subvault_Domain.Entities;
using System;
using System.Collections.Generic;

namespace Subvault_Domain.APIEntities {

    public class MovieApiResult {

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
    }
}
