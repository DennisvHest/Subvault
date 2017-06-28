using Subvault_Domain.Entities;
using System;
using System.Collections.Generic;

namespace Subvault_UI.Models {

    public class MovieViewModel {

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string PosterURL { get; set; }
        public string BackdropURL { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
    }
}