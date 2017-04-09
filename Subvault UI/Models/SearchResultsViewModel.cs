using Subvault_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subvault_UI.Models {
    public class SearchResultsViewModel {

        public IEnumerable<Item> FoundItems { get; set; }
        public string Title { get; set; }
    }
}