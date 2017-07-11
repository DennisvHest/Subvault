using Subvault_UI.BusinessLogic;
using System.Web.Mvc;

namespace Subvault_UI.Controllers {

    public class HomeController : Controller {

        private ItemManager movieManager;

        public HomeController(ItemManager movieManager) {
            this.movieManager = movieManager;
        }

        /// <author>Dennis van Hest</author>
        /// <summary>
        /// Returns the view for the index with the IndexViewModel
        /// </summary>
        /// <returns>The view</returns>
        public ViewResult Index() {
            ViewBag.Type = "Movie";
            return View(movieManager.GetIndexViewModel());
        }

        public ViewResult SeriesIndex() {
            ViewBag.Type = "Series";
            return View("Index", movieManager.GetSeriesIndexViewModel());
        }
    }
}