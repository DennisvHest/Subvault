using Subvault_Domain.Entities;

namespace Subvault_Domain.Abstract {

    public interface ISubtitlesRepository {

        void CreateSubtitlesForMovie(Subtitles subtitles);
        void CreateSubtitlesForSeries(Subtitles subtitles);
    }
}
