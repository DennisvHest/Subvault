using Subvault_UI.BusinessLogic;
using Subvault_UI.Models;
using System.Web.Mvc;

namespace Subvault_UI.Controllers {

    public class MovieController : Controller {

        private ItemManager movieManager;

        public MovieController(ItemManager movieManager) {
            this.movieManager = movieManager;
        }

        /// <author>Dennis van Hest</author>
        /// <summary>
        /// Returns the detail view of the movie with the fiven id
        /// </summary>
        /// <param name="id">Id of the movie</param>
        /// <returns>The view</returns>
        public ViewResult Detail(int id) {
            MovieViewModel movie = movieManager.GetMovieById(id);

            return View(movie);
        }
    }
}
