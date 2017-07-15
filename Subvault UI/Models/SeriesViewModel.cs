using Subvault_Domain.Entities;
using System.Collections.Generic;

namespace Subvault_UI.Models {

    public class SeriesViewModel : ItemViewModel {
        
        public IEnumerable<Season> Seasons { get; set; }
    }
}