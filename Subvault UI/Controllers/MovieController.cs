using Subvault_UI.BusinessLogic;
using Subvault_UI.Models;
using System.Web.Mvc;

namespace Subvault_UI.Controllers {

    public class MovieController : Controller {

        private ItemManager movieManager;

        public MovieController(ItemManager movieManager) {
            this.movieManager = movieManager;
        }

        public ViewResult Detail(int id) {
            MovieViewModel movie = movieManager.GetMovieById(id);

            return View(movie);
        }
    }
}
