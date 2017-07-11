using Subvault_Domain.APIEntities;
using Subvault_Domain.Entities;

namespace Subvault_Domain.ObjectMappers {

    public class APISeriesMapper {

        /// <author>Dennis van Hest</author>
        /// <summary>
        /// Maps the API's movie result to a Movie object
        /// </summary>
        /// <param name="movieResult">The movie result from the API</param>
        /// <returns>A Movie object</returns>
        public Series mapMovie(PopularSeriesResult movieResult) {
            return new Series {
                Id = movieResult.id,
                Title = movieResult.name,
                PosterPath = movieResult.poster_path
            };
        }
    }
}
