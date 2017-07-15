using Subvault_Domain;
using Subvault_Domain.Entities;
using Subvault_UI.ActionFilters;
using Subvault_UI.BusinessLogic;
using System.Web;
using System.Web.Mvc;

namespace Subvault_UI.Controllers {

    public class SubtitlesController : Controller {

        private ItemManager ItemManager;
        private SubtitlesManager SubtitlesManager;

        public SubtitlesController(ItemManager itemManager, SubtitlesManager subtitlesManager) {
            ItemManager = itemManager;
            SubtitlesManager = subtitlesManager;
        }

        /// <author>Dennis van Hest</author>
        /// <summary>
        /// Returns the view for uploading subtitles
        /// </summary>
        /// <returns>The view</returns>
        [HttpGet]
        [UserLoggedIn]
        public ViewResult UploadForMovie() {
            Logger.Log.InfoFormat(Logger.Format + "GET Request for UploadForMovie", GetType().ToString());
            ViewBag.Type = "Movie";
            return View("Upload");
        }

        [HttpGet]
        [UserLoggedIn]
        public ViewResult UploadForSeries() {
            Logger.Log.InfoFormat(Logger.Format + "GET Request for UploadForSeries", GetType().ToString());
            ViewBag.Type = "Series";
            return View("Upload");
        }


        /// <author>Dennis van Hest</author>
        /// <summary>
        /// Catches the request to upload subtitles
        /// </summary>
        /// <param name="subtitles">Subtitles data</param>
        /// <param name="itemId">The id of the movie the subtitles belong to</param>
        /// <param name="file">The subtitles file</param>
        /// <returns>Redirects to the homepage</returns>
        [HttpPost]
        [UserLoggedIn]
        public ActionResult UploadForMovie(Subtitles subtitles, int itemId, HttpPostedFileBase file) {
            Logger.Log.InfoFormat(Logger.Format + "POST Request for UploadForMovie for movie with id: " + itemId + " and subtitles: " + subtitles.ToString(), GetType().ToString());
            SubtitlesManager.UploadForMovie(subtitles, itemId, file);
            TempData["upload-success"] = true;
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [UserLoggedIn]
        public ActionResult UploadForSeries(Subtitles subtitles, int itemId, int episodeId, HttpPostedFileBase file) {
            Logger.Log.InfoFormat(Logger.Format + "POST Request for UploadForSeries for series with id: " + itemId + " and episode with id: " + episodeId + " and subtitles: " + subtitles.ToString(), GetType().ToString());
            SubtitlesManager.UploadForSeries(subtitles, itemId, episodeId, file);
            TempData["upload-success"] = true;
            return RedirectToAction("Index", "Home");
        }

        public FileResult Download(string filePath, string fileName) {
            Logger.Log.InfoFormat(Logger.Format + "Download request for fileName: " + fileName + " and filePath: " + filePath, GetType().ToString());
            string contentType = "text/plain";
            return File(filePath, contentType, fileName);
        }
    }
}