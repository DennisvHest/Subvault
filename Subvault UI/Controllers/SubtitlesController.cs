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
            ViewBag.Type = "Movie";
            return View("Upload");
        }

        [HttpGet]
        [UserLoggedIn]
        public ViewResult UploadForSeries() {
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
            SubtitlesManager.UploadForMovie(subtitles, itemId, file);
            TempData["upload-success"] = true;
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [UserLoggedIn]
        public ActionResult UploadForSeries(Subtitles subtitles, int itemId, int episodeId, HttpPostedFileBase file) {
            SubtitlesManager.UploadForSeries(subtitles, itemId, episodeId, file);
            TempData["upload-success"] = true;
            return RedirectToAction("Index", "Home");
        }

        public FileResult Download(string filePath, string fileName) {
            string contentType = "text/plain";
            return File(filePath, contentType, fileName);
        }
    }
}