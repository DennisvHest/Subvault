using Newtonsoft.Json;
using RestSharp;
using Subvalut_Domain.APIEntities;
using Subvault_Domain.Abstract;
using Subvault_Domain.Entities;
using Subvault_Domain.ObjectMappers;
using Subvault_UI.Models;
using System.Collections.Generic;
using System.Linq;

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

        /// <author>Dennis van Hest</author>
        /// <summary>
        /// Creates the view model for the home page
        /// </summary>
        /// <returns>The IndexViewModel</returns>
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

        /// <author>Dennis van Hest</author>
        /// <summary>
        /// Returns the view model for the Movie detail page for the given movie id
        /// </summary>
        /// <param name="id">Id of the movie</param>
        /// <returns>The MovieViewModel</returns>
        public MovieViewModel GetMovieById(int id) {
            Movie movie = itemRepo.GetMovieById(id);

            return new MovieViewModel {
                Title = movie.Title,
                Description = movie.Description,
                ReleaseDate = movie.ReleaseDate,
                PosterURL = movie.PosterURL,
                BackdropURL = movie.BackdropURL,
                Genres = movie.Genres,
                CastMembers = movie.People.Where(p => p is CastMember).AsEnumerable().Cast<CastMember>(),
                Directors = movie.People.Where(p => p is Director).AsEnumerable().Cast<Director>()
            };
        }

        /// <author>Dennis van Hest</author>
        /// <summary>
        /// Creates the view model for the search results page with the fiven query
        /// </summary>
        /// <param name="query">The query</param>
        /// <returns>The SearchResultsViewModel</returns>
        public SearchResultsViewModel Search(SearchQuery query) {
            return new SearchResultsViewModel {
                FoundItems = itemRepo.SearchItemsByTitle(query.Title),
                Title = query.Title
            };
        }

        /// <author>Dennis van Hest</author>
        /// <summary>
        /// Returns a list of Items for the API
        /// </summary>
        /// <param name="query">Title of the item</param>
        /// <returns>The list of ItemAPIModels</returns>
        public IEnumerable<ItemAPIModel> Search(string query) {
            List<ItemAPIModel> itemList = new List<ItemAPIModel>();

            IEnumerable<Item> result = itemRepo.SearchItemsByTitle(query);

            foreach (Item item in result) {
                ItemAPIModel itemApiModel = new ItemAPIModel {
                    Id = item.Id,
                    Title = item.Title,
                    PosterURL = item.PosterURL
                };

                itemList.Add(itemApiModel);
            }

            return itemList;
        }
    }
}