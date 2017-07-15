using Subvault_Domain;
using System.IO;
using System.Web;

namespace Subvault_UI.BusinessLogic {

    public class FileManager {

        public FileManager() { }

        /// <author>Dennis van Hest</author>
        /// <summary>
        /// Saves the subtitles file into the given folder
        /// </summary>
        /// <param name="file">File to be saved</param>
        /// <param name="folder">Folder in which the file will be saved</param>
        /// <returns>The path to locate the saved file</returns>
        public string SaveSubtitlesInFolder(HttpPostedFileBase file, string folder) {
            Logger.Log.InfoFormat(Logger.Format + "Saving file with fileName: " + file.FileName + " to folder: " + GlobalSettings.SubtitlesFileRoot + folder, GetType().ToString());

            //Create the folder if it does'nt already exist
            string folderPath = HttpContext.Current.Server.MapPath(GlobalSettings.SubtitlesFileRoot + folder);
            Directory.CreateDirectory(folderPath);

            //Create a unique filename
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.FileName);
            string fileName = fileNameWithoutExtension + System.DateTime.Now.ToString("_ddMMyyhhmmssfff") + Path.GetExtension(file.FileName);

            //Save the file
            string path = Path.Combine(folderPath + "/", fileName);
            file.SaveAs(path);

            return GlobalSettings.SubtitlesFileRoot + folder + "/" + fileName;
        }
    }
}