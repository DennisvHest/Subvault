using System.Collections.Generic;

namespace Subvalut_Domain.APIEntities {

    public class PopularMoviesRoot {
        public int page { get; set; }
        public List<MovieResult> results { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
    }
}
