using Subvault_UI.BusinessLogic;
using Subvault_UI.Models;
using System.Web.Mvc;

namespace Subvault_UI.Controllers {

    public class SearchController : Controller {

        private ItemManager itemManager;

        public SearchController(ItemManager itemManager) {
            this.itemManager = itemManager;
        }

        public ViewResult Search(SearchQuery query) {
            return View("SearchResults", itemManager.Search(query));
        }
    }
}