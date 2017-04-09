﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subvalut_Domain.APIEntities {

    public class PopularMoviesRoot {
        public int page { get; set; }
        public List<MovieResult> results { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
    }
}