using Subvault_UI.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Subvault_UI.Controllers {

    public class HomeController : Controller {

        private ItemManager movieManager;

        public HomeController(ItemManager movieManager) {
            this.movieManager = movieManager;
        }

        /* Author: Dennis van Hest
         * Returns the view for the index with the IndexViewModel
         */
        public ViewResult Index() {
            return View(movieManager.GetIndexViewModel());
        }
    }
}