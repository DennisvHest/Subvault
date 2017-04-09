using Subvault_Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Subvault_Domain.Entities;

namespace Subvault_Domain.Concrete {
    public class ItemRepository : IItemRepository {

        private EFDbContext context;

        public ItemRepository(EFDbContext context) {
            this.context = context;
        }

        /* Author: Dennis van Hest
         * Returns a list of items that contain the title string
         */
        public IEnumerable<Item> SearchItemsByTitle(string title) {
            return context.Items
                .Where(i => i.Title.Contains(title));
        }
    }
}
