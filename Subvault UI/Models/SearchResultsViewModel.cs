using Subvault_Domain.Entities;
using System.Collections.Generic;

namespace Subvault_UI.Models {
    public class SearchResultsViewModel {

        public IEnumerable<Item> FoundItems { get; set; }
        public string Title { get; set; }
    }
}