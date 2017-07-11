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
            RestClient client = new RestClient(GlobalSettings.APIRoot + "/movie/popular?page=1&language=en-US&api_key=" + GlobalSettings.APIKey);
            RestRequest request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            return response;
        }

        public IRestResponse GetPopularSeries() {
            RestClient client = new RestClient(GlobalSettings.APIRoot + "/tv/popular?page=1&language=en-US&api_key=" + GlobalSettings.APIKey);
            RestRequest request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            return response;
        }

        public IRestResponse GetMovieById(int id) {
            RestClient client = new RestClient(GlobalSettings.APIRoot + "/movie/" + id + "?language=en-US&api_key=" + GlobalSettings.APIKey);
            RestRequest request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            return response;
        }

        public IRestResponse GetSeriesById(int id) {
            RestClient client = new RestClient(GlobalSettings.APIRoot + "/tv/" + id + "?language=en-US&api_key=" + GlobalSettings.APIKey);
            RestRequest request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            return response;
        }

        public IRestResponse GetCreditsByMovieId(int id) {
            RestClient client = new RestClient(GlobalSettings.APIRoot + "/movie/" + id + "/credits?api_key=" + GlobalSettings.APIKey);
            RestRequest request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            return response;
        }

        public IRestResponse GetCreditsBySeriesId(int id) {
            RestClient client = new RestClient(GlobalSettings.APIRoot + "/tv/" + id + "/credits?api_key=" + GlobalSettings.APIKey);
            RestRequest request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            return response;
        }

        public IRestResponse SearchMovies(string query, int pageNr) {
            RestClient client = new RestClient(GlobalSettings.APIRoot + "/search/movie" + "?api_key=" + GlobalSettings.APIKey + "&language=en-US&query=" + query + "&page=" + pageNr);
            RestRequest request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            return response;
        }
    }
}
