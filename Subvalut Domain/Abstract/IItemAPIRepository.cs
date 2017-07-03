using RestSharp;

namespace Subvault_Domain.Abstract {

    public interface IItemAPIRepository {

        IRestResponse GetPopularMovies();
        IRestResponse GetMovieById(int id);
        IRestResponse GetCreditsByMovieId(int id);
        IRestResponse SearchMovies(string query, int pageNr);
    }
}
