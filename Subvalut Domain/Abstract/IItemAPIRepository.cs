using RestSharp;
using Subvault_Domain.Entities;
using System.Collections.Generic;

namespace Subvault_Domain.Abstract {

    public interface IItemAPIRepository {

        IEnumerable<Item> Items { get; }

        IRestResponse getPopularMovies();
    }
}
