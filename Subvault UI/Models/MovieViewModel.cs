using Subvault_Domain.Entities;
using System;
using System.Collections.Generic;

namespace Subvault_UI.Models {

    public class MovieViewModel : ItemViewModel {

        public IEnumerable<Subtitles> Subtitles { get; set; }
    }
}