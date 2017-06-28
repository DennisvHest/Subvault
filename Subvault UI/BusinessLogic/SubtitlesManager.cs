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