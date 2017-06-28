using Subvault_Domain;
using System.IO;
using System.Web;

namespace Subvault_UI.BusinessLogic {

    public class FileManager {

        public FileManager() { }

        public string SaveSubtitlesInFolder(HttpPostedFileBase file, string folder) {
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