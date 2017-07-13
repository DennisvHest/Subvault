using System.Collections.Generic;

namespace Subvault_UI.Models {

    public class SeriesAPIModel : ItemAPIModel {

        public List<SeasonAPIModel> Seasons { get; set; }
    }
}