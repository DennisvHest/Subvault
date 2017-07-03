using Newtonsoft.Json;
using Subvault_Domain.Entities;
using System.Collections.Generic;

namespace Subvault_Domain.APIEntities {

    public class MovieSearchResultsRoot {

        public int Page { get; set; }

        [JsonProperty("tptal_results")]
        public int TotalResults { get; set; }

        [JsonProperty("tptal_pages")]
        public int TotalPages { get; set; }

        public List<Movie> Results { get; set; }
    }
}
