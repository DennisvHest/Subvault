using System.Collections.Generic;

namespace Subvault_Domain.APIEntities {

    public class PopularMoviesRoot {
        public int page { get; set; }
        public List<PopularMovieResult> results { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
    }
}
