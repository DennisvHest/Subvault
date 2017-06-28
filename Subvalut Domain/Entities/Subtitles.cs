namespace Subvault_Domain.Entities {

    public class Subtitles {

        public int Id { get; set; }
        public string Language { get; set; }
        public string SyncType { get; set; }
        public bool ForHearingImpaired { get; set; }
        public bool IsForeignOnly { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }

        //Foreign key to User
        public virtual User Uploader { get; set; }

        //Foreign key to Movie
        public virtual Movie Item { get; set; }
    }
}
