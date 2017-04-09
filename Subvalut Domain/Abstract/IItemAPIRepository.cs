using RestSharp;
using Subvault_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subvault_Domain.Abstract {

    public interface IItemAPIRepository {

        IEnumerable<Item> Items { get; }

        IRestResponse getPopularMovies();
    }
}
