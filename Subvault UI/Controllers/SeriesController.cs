using Subvault_Domain;
using Subvault_UI.BusinessLogic;
using Subvault_UI.Models;
using System.Web.Mvc;

namespace Subvault_UI.Controllers {

    public class SeriesController : Controller {

        private ItemManager seriesManager;

        public SeriesController(ItemManager movieManager) {
            this.seriesManager = movieManager;
        }

        public ViewResult Detail(int id, int? seasonNumber, int? episodeNumber) {
            Logger.Log.InfoFormat(Logger.Format + "Request for details for series id: " + id, GetType().ToString());

            SeriesViewModel series = seriesManager.GetSeriesById(id, seasonNumber, episodeNumber);

            if (seasonNumber != null) {
                ViewBag.Season = true;
            }

            if (episodeNumber != null) {
                ViewBag.Episode = true;
            }

            return View("~/Views/Item/Detail.cshtml", series);
        }
    }
}
