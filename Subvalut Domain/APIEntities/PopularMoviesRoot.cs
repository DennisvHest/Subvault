﻿using Subvault_Domain.Entities;
using System.Collections.Generic;

namespace Subvault_Domain.APIEntities {

    public class PopularMoviesRoot {
        public int Page { get; set; }
        public List<Movie> Results { get; set; }
        public int Total_Results { get; set; }
        public int Total_Pages { get; set; }
    }
}
