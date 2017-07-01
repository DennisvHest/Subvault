using Subvalut_Domain.APIEntities;
using Subvault_Domain.Entities;

namespace Subvault_Domain.ObjectMappers {

    public class APIMovieMapper {

        /// <author>Dennis van Hest</author>
        /// <summary>
        /// Maps the API's movie result to a Movie object
        /// </summary>
        /// <param name="movieResult">The movie result from the API</param>
        /// <returns>A Movie object</returns>
        public Movie mapMovie(MovieResult movieResult) {
            return new Movie {
                Id = movieResult.id,
                Title = movieResult.title,
                PosterURL = movieResult.poster_path
            };
        }
    }
}
