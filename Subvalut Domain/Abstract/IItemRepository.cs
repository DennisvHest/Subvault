using Subvault_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subvault_Domain.Abstract {

    public interface IItemRepository {

        IEnumerable<Item> SearchItemsByTitle(string title);
    }
}
