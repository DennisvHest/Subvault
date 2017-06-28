using Subvault_Domain.Abstract;
using Subvault_Domain.Entities;
using System.Linq;

namespace Subvault_Domain.Concrete {

    public class SubtitlesRepository : ISubtitlesRepository {

        private EFDbContext context;

        public SubtitlesRepository(EFDbContext context) {
            this.context = context;
        }

        public void CreateSubtitles(Subtitles subtitles) {
            Item item = context.Items.SingleOrDefault(i => i.Id == subtitles.Item.Id);
            User user = context.Users.SingleOrDefault(u => u.Username == subtitles.Uploader.Username);

            subtitles.Item = (Movie) item;
            subtitles.Uploader = user;

            context.Subtitles.Add(subtitles);
            context.SaveChanges();
        }
    }
}
