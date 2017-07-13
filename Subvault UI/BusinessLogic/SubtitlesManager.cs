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
        /// <param name="itemId">The id of the movie the subtitles belong to</param>
        /// <param name="file">The subtitles file</param>
        public void UploadForMovie (Subtitles subtitles, int itemId, HttpPostedFileBase file) {
            //Save the file
            string filePath = FileManager.SaveSubtitlesInFolder(file, itemId.ToString());
            UserSessionViewModel loggedInUser = (UserSessionViewModel)HttpContext.Current.Session["Login"];

            subtitles.FilePath = filePath;
            subtitles.FileName = file.FileName;
            subtitles.Movie = new Movie { Id = itemId };
            subtitles.Uploader = new User { Username = loggedInUser.Username };

            //Add the subtitles to the database
            subtitlesRepo.CreateSubtitlesForMovie(subtitles);
        }

        public void UploadForSeries(Subtitles subtitles, int itemId, int episodeId, HttpPostedFileBase file) {
            //Save the file
            string filePath = FileManager.SaveSubtitlesInFolder(file, itemId + "/" + episodeId);
            UserSessionViewModel loggedInUser = (UserSessionViewModel)HttpContext.Current.Session["Login"];

            subtitles.FilePath = filePath;
            subtitles.FileName = file.FileName;
            subtitles.Episode = new Episode { Id = episodeId };
            subtitles.Uploader = new User { Username = loggedInUser.Username };

            //Add the subtitles to the database
            subtitlesRepo.CreateSubtitlesForSeries(subtitles);
        }
    }
}