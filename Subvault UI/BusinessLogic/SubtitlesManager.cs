using Subvault_Domain.Abstract;
using Subvault_Domain.Entities;
using Subvault_UI.Models;
using System.Web;

namespace Subvault_UI.BusinessLogic {

    public class SubtitlesManager {

        private FileManager FileManager;
        private ISubtitlesRepository subtitlesRepo;

        public SubtitlesManager(FileManager fileManager, ISubtitlesRepository subtitlesRepository) {
            FileManager = fileManager;
            subtitlesRepo = subtitlesRepository;
        }

        /// <author>Dennis van Hest</author>
        /// <summary>
        /// Uploads the given subtitles to the server
        /// </summary>
        /// <param name="subtitles">Subtitle data</param>
        /// <param name="movieId">The id of the movie the subtitles belong to</param>
        /// <param name="file">The subtitles file</param>
        public void Upload (Subtitles subtitles, int movieId, HttpPostedFileBase file) {
            //Save the file
            string filePath = FileManager.SaveSubtitlesInFolder(file, movieId.ToString());
            UserSessionViewModel loggedInUser = (UserSessionViewModel)HttpContext.Current.Session["Login"];

            subtitles.FilePath = filePath;
            subtitles.FileName = file.FileName;
            subtitles.Item = new Movie { Id = movieId };
            subtitles.Uploader = new User { Username = loggedInUser.Username };

            //Add the subtitles to the database
            subtitlesRepo.CreateSubtitles(subtitles);
        }
    }
}