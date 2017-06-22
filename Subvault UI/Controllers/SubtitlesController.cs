using Subvault_UI.ActionFilters;
using Subvault_UI.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Subvault_UI.Controllers {

    public class SubtitlesController : Controller {

        private ItemManager ItemManager;

        public SubtitlesController(ItemManager itemManager) {
            ItemManager = itemManager;
        }

        [HttpGet]
        [UserLoggedIn]
        public ViewResult Upload() {
            return View();
        }
    }
}