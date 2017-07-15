using Subvault_Domain;
using Subvault_UI.BusinessLogic;
using Subvault_UI.Models;
using System.Web.Mvc;

namespace Subvault_UI.Controllers {

    public class SearchController : Controller {

        private ItemManager itemManager;

        public SearchController(ItemManager itemManager) {
            this.itemManager = itemManager;
        }

        /// <author>Dennis van Hest</author>
        /// <summary>
        /// Returns the view for the search results from the given query
        /// </summary>
        /// <param name="query">The search query</param>
        /// <returns>The view</returns>
        public ViewResult Search(SearchQuery query) {
            Logger.Log.InfoFormat(Logger.Format + "Search request for: " + query.Title, GetType().ToString());
            return View("SearchResults", itemManager.Search(query));
        }
    }
}