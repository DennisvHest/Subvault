using Subvault_Domain.Entities;
using System.Collections.Generic;

namespace Subvault_Domain.APIEntities {

    public class PopularSeriesResult {
        public string poster_path { get; set; }
        public string overview { get; set; }
        public string release_date { get; set; }
        public List<int> genre_ids { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string backdrop_path { get; set; }
    }
}
