using Newtonsoft.Json;
using RestSharp;
using Subvault_Domain.APIEntities;
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
            IRestResponse popularMoviesResponse = itemApiRepo.GetPopularMovies();

            //Convert the result from JSON to a PopularMoviesRoot object
            PopularMoviesRoot popularMoviesRoot = JsonConvert.DeserializeObject<PopularMoviesRoot>(popularMoviesResponse.Content);

            //Convert the PopularMoviesRoot object to a list with movies
            List<Movie> popularMovies = new List<Movie>();

            foreach (PopularMovieResult movieResult in popularMoviesRoot.results) {
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

            //If the movie is not fou;nd in the database, get the movie from the API
            if (movie == null) {
                movie = GetMovieByIdFromAPI(id);
                itemRepo.InsertItem(movie);
            }

            //IEnumerable<CastMember> castMembers = movie.People.Where(p => p is CastMember).AsEnumerable().Cast<CastMember>().Take(5);

            //IEnumerable<CrewMember> crewMembers = movie.People.Where(p => p is CrewMember).AsEnumerable().Cast<CrewMember>();
            //IEnumerable<CrewMember> directors = crewMembers.Where(p => p.Job == "Director");

            return new MovieViewModel {
                Title = movie.Title,
                Description = movie.Description,
                ReleaseDate = movie.ReleaseDate,
                PosterURL = movie.PosterURL,
                BackdropURL = movie.BackdropURL,
                Genres = movie.Genres,
                Subtitles = movie.Subtitles
            };
        }

        private Movie GetMovieByIdFromAPI(int id) {
            //Retrieve the movie from the API
            IRestResponse movieResponse = itemApiRepo.GetMovieById(id);

            //Convert the response to a movie object
            Movie movie = JsonConvert.DeserializeObject<Movie>(movieResponse.Content);

            //TODO: Uncomment and add genres to movie

            //Retrieve the credits (Person objects) from the API
            //IRestResponse creditsResponse = itemApiRepo.GetCreditsByMovieId(id);

            ////Convert the response to a MovieCreditsRoot object
            //MovieCreditsRoot movieCreditsRoot = JsonConvert.DeserializeObject<MovieCreditsRoot>(creditsResponse.Content);

            ////Add all people to the movie
            //movie.ItemCrewMembers = new List<ItemCrewMember>();

            //foreach (CrewMemberResult crewMember in movieCreditsRoot.Crew) {
            //    movie.ItemCrewMembers.Add(new ItemCrewMember { ItemId = movie.Id, CrewMemberId = crewMember.Id, Item = movie, CrewMember = new CrewMember { Id = crewMember.Id, Name = crewMember.Name }, Job = crewMember.Job });
            //}

            //movie.ItemCastMembers = new List<ItemCastMember>();

            movie.Genres = null;

            return movie;
        }

        /// <author>Dennis van Hest</author>
        /// <summary>
        /// Creates the view model for the search results page with the given query
        /// </summary>
        /// <param name="query">The query</param>
        /// <returns>The SearchResultsViewModel</returns>
        public SearchResultsViewModel Search(SearchQuery query) {
            IRestResponse response = itemApiRepo.SearchMovies(query.Title, 1);

            MovieSearchResultsRoot movieSearchResultsRoot = JsonConvert.DeserializeObject<MovieSearchResultsRoot>(response.Content);

            return new SearchResultsViewModel {
                FoundItems = movieSearchResultsRoot.Results,
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