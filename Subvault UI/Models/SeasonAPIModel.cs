using System.Collections.Generic;

namespace Subvault_UI.Models {

    public class SeasonAPIModel {

        public int Id { get; set; }
        public int SeasonNumber { get; set; }

        public List<EpisodeAPIModel> Episodes { get; set; }
    }
}