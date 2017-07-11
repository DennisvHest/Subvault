using RestSharp;

namespace Subvault_Domain.Abstract {

    public interface IItemAPIRepository {

        IRestResponse GetPopularMovies();
        IRestResponse GetPopularSeries();
        IRestResponse GetMovieById(int id);
        IRestResponse GetSeriesById(int id);
        IRestResponse GetCreditsByMovieId(int id);
        IRestResponse GetCreditsBySeriesId(int id);
        IRestResponse SearchMovies(string query, int pageNr);
    }
}
