using Subvault_Domain.Abstract;
using System.Collections.Generic;
using Subvault_Domain.Entities;
using RestSharp;
using System;

namespace Subvault_Domain.Concrete {

    public class ItemAPIRepository : IItemAPIRepository {
        public IEnumerable<Item> Items { get; }

        /// <author>Dennis van Hest</author>
        /// <summary>
        /// Gets the API's JSON response with the first page of the popular movies
        /// </summary>
        /// <returns>Response object</returns>
        public IRestResponse GetPopularMovies() {
            Logger.Log.InfoFormat(Logger.Format + "Requesting popular movies from API", GetType().ToString());

            RestClient client = new RestClient(GlobalSettings.APIRoot + "/movie/popular?page=1&language=en-US&api_key=" + GlobalSettings.APIKey);
            RestRequest request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            return response;
        }

        public IRestResponse GetPopularSeries() {
            Logger.Log.InfoFormat(Logger.Format + "Requesting popular series from API", GetType().ToString());

            RestClient client = new RestClient(GlobalSettings.APIRoot + "/tv/popular?page=1&language=en-US&api_key=" + GlobalSettings.APIKey);
            RestRequest request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            return response;
        }

        public IRestResponse GetMovieById(int id) {
            Logger.Log.InfoFormat(Logger.Format + "Requesting movie with id: " + id + " from API", GetType().ToString());

            RestClient client = new RestClient(GlobalSettings.APIRoot + "/movie/" + id + "?language=en-US&api_key=" + GlobalSettings.APIKey);
            RestRequest request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            return response;
        }

        public IRestResponse GetSeriesById(int id) {
            Logger.Log.InfoFormat(Logger.Format + "Requesting series with id: " + id + " from API", GetType().ToString());

            RestClient client = new RestClient(GlobalSettings.APIRoot + "/tv/" + id + "?language=en-US&api_key=" + GlobalSettings.APIKey);
            RestRequest request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            return response;
        }

        public IRestResponse GetSeriesSeasonById(int seriesId, int seasonId) {
            Logger.Log.InfoFormat(Logger.Format + "Requesting season with id: " + seasonId + " for series with id: " + seriesId + " from API", GetType().ToString());

            RestClient client = new RestClient(GlobalSettings.APIRoot + "/tv/" + seriesId + "/season/" + seasonId + "?language=en-US&api_key=" + GlobalSettings.APIKey);
            RestRequest request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            return response;
        }

        public IRestResponse GetCreditsByMovieId(int id) {
            Logger.Log.InfoFormat(Logger.Format + "Requesting credits for movie with id: " + id + " from API", GetType().ToString());

            RestClient client = new RestClient(GlobalSettings.APIRoot + "/movie/" + id + "/credits?api_key=" + GlobalSettings.APIKey);
            RestRequest request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            return response;
        }

        public IRestResponse GetCreditsBySeriesId(int id) {
            Logger.Log.InfoFormat(Logger.Format + "Requesting credits for series with id: " + id + " from API", GetType().ToString());

            RestClient client = new RestClient(GlobalSettings.APIRoot + "/tv/" + id + "/credits?api_key=" + GlobalSettings.APIKey);
            RestRequest request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            return response;
        }

        public IRestResponse SearchMovies(string query, int pageNr) {
            Logger.Log.InfoFormat(Logger.Format + "Requesting page: " + pageNr + " of movies with search query: " + query, GetType().ToString());

            RestClient client = new RestClient(GlobalSettings.APIRoot + "/search/movie" + "?api_key=" + GlobalSettings.APIKey + "&language=en-US&query=" + query + "&page=" + pageNr);
            RestRequest request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            return response;
        }
    }
}
