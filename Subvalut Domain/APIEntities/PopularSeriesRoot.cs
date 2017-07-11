using Subvault_Domain.Entities;
using System.Collections.Generic;

namespace Subvault_Domain.APIEntities {
    public class PopularSeriesRoot {

        public int Page { get; set; }
        public List<PopularSeriesResult> Results { get; set; }
        public int Total_Results { get; set; }
        public int Total_Pages { get; set; }
    }
}
