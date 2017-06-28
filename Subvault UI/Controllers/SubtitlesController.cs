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

        [HttpGet]
        [UserLoggedIn]
        public ViewResult Upload() {
            return View();
        }

        [HttpPost]
        [UserLoggedIn]
        public ActionResult Upload(Subtitles subtitles, int movieId, HttpPostedFileBase file) {
            SubtitlesManager.Upload(subtitles, movieId, file);
            TempData["upload-success"] = true;
            return RedirectToAction("Index", "Home");
        }
    }
}