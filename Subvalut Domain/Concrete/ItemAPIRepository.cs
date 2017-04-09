using Subvault_Domain.Abstract;
using System.Collections.Generic;
using Subvault_Domain.Entities;
using System.Threading.Tasks;
using RestSharp;
using Subvalut_Domain;

namespace Subvault_Domain.Concrete {

    public class ItemAPIRepository : IItemAPIRepository {
        public IEnumerable<Item> Items { get; }

        /* Author: Dennis van Hest
         * Returns the API's JSON response with the first page of the popular movies
         */
        public IRestResponse getPopularMovies() {
            var client = new RestClient(GlobalSettings.APIRoot + "/movie/popular?page=1&language=en-US" + GlobalSettings.APIKey);
            var request = new RestRequest(Method.GET);
            request.AddParameter("undefined", "{}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            return response;
        }
    }
}
