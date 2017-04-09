using Subvalut_Domain.APIEntities;
using Subvault_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subvault_Domain.ObjectMappers {

    public class APIMovieMapper {

        public Movie mapMovie(MovieResult movieResult) {
            return new Movie {
                Id = movieResult.id,
                Title = movieResult.title,
                PosterURL = movieResult.poster_path
            };
        }
    }
}
