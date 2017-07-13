using Subvault_Domain.Entities;
using System.Collections.Generic;

namespace Subvault_Domain.Abstract {

    public interface IItemRepository {

        Movie GetMovieById(int id);
        Series GetSeriesById(int id);
        IEnumerable<Movie> SearchMoviesByTitle(string title);
        IEnumerable<Series> SearchSeriesByTitle(string title);
        void InsertItem(Item item);
    }
}
