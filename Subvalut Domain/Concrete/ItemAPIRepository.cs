using Subvault_Domain.Abstract;
using System.Collections.Generic;
using Subvault_Domain.Entities;
using RestSharp;

namespace Subvault_Domain.Concrete {

    public class ItemAPIRepository : IItemAPIRepository {
        public IEnumerable<Item> Items { get; }

        /// <author>Dennis van Hest</author>
        /// <summary>
        /// Gets the API's JSON response with the first page of the popular movies
        /// </summary>
        /// <returns>Response object</returns>
        public IRestResponse getPopularMovies() {
            var client = new RestClient(GlobalSettings.APIRoot + "/movie/popular?page=1&language=en-US" + GlobalSettings.APIKey);
            var request = new RestRequest(Method.GET);
            request.AddParameter("undefined", "{}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            return response;
        }
    }
}
