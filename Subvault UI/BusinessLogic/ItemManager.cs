using Newtonsoft.Json;
using RestSharp;
using Subvalut_Domain.APIEntities;
using Subvault_Domain;
using Subvault_Domain.Abstract;
using Subvault_Domain.Concrete;
using Subvault_Domain.Entities;
using Subvault_Domain.ObjectMappers;
using Subvault_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subvault_UI.BusinessLogic {

    public class ItemManager {

        public IItemAPIRepository itemApiRepo;
        public IItemRepository itemRepo;
        private APIMovieMapper apiMovieMapper;

        public ItemManager(IItemAPIRepository itemApiRepo, IItemRepository itemRepo) {
            this.itemApiRepo = itemApiRepo;
            this.itemRepo = itemRepo;

            apiMovieMapper = new APIMovieMapper();
        }

        /* Author: Dennis van Hest
         * Returns the view model required for the index page
         */
        public IndexViewModel GetIndexViewModel() {
            //Retrieve the API popular movies result
            IRestResponse popularMoviesResponse = itemApiRepo.getPopularMovies();

            //Convert the result from JSON to a PopularMoviesRoot object
            PopularMoviesRoot popularMoviesRoot = JsonConvert.DeserializeObject<PopularMoviesRoot>(popularMoviesResponse.Content);

            //Convert the PopularMoviesRoot object to a list with movies
            List<Movie> popularMovies = new List<Movie>();

            foreach (MovieResult movieResult in popularMoviesRoot.results) {
                popularMovies.Add(apiMovieMapper.mapMovie(movieResult));
            }

            //Create the viewmodel
            IndexViewModel indexViewModel = new IndexViewModel { PopularMovies = popularMovies.Take(6) };

            return indexViewModel;
        }

        /* Author: Dennis van Hest
         * Returns the view model with the search results from the search query
         */
        public SearchResultsViewModel Search(SearchQuery query) {
            return new SearchResultsViewModel {
                FoundItems = itemRepo.SearchItemsByTitle(query.Title),
                Title = query.Title
            };
        }

        public IEnumerable<Item> Search(string query) {
            return itemRepo.SearchItemsByTitle(query);
        }
    }
}