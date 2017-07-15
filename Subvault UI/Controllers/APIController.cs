using Subvault_Domain;
using Subvault_UI.BusinessLogic;
using Subvault_UI.Models;
using System.Linq;
using System.Web.Http;

namespace Subvault_UI.Controllers {

    public class APIController : ApiController {

        private ItemManager itemManager;

        public APIController(ItemManager itemManager) {
            this.itemManager = itemManager;
        }

        /// <author>Dennis van Hest</author>
        /// <summary>
        /// Returns a list of Items with containing the given query
        /// </summary>
        /// <param name="query">The query</param>
        /// <returns>A list of ItemAPIModels</returns>
        [HttpGet]
        public ItemAPIModel[] SearchMovies(string query) {
            Logger.Log.InfoFormat(Logger.Format + "API Request for SearchMovies with query: " + query, GetType().ToString());
            return itemManager.SearchMovies(query).ToArray();
        }

        [HttpGet]
        public SeriesAPIModel[] SearchSeries(string query) {
            Logger.Log.InfoFormat(Logger.Format + "API Request for SearchSeries with query: " + query, GetType().ToString());
            return itemManager.SearchSeries(query).ToArray();
        }
    }
}
