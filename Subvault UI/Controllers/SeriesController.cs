using Subvault_UI.BusinessLogic;
using Subvault_UI.Models;
using System.Web.Mvc;

namespace Subvault_UI.Controllers {

    public class SeriesController : Controller {

        private ItemManager seriesManager;

        public SeriesController(ItemManager movieManager) {
            this.seriesManager = movieManager;
        }

        public ViewResult Detail(int id) {
            SeriesViewModel series = seriesManager.GetSeriesById(id);

            return View("~/Views/Item/Detail.cshtml", series);
        }
    }
}
