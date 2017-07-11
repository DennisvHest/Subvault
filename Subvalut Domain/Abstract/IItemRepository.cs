using Subvault_Domain.Entities;
using System.Collections.Generic;

namespace Subvault_Domain.Abstract {

    public interface IItemRepository {

        Movie GetMovieById(int id);
        Series GetSeriesById(int id);
        IEnumerable<Item> SearchItemsByTitle(string title);
        void InsertItem(Item item);
    }
}
