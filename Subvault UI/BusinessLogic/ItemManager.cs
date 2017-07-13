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
        private APISeriesMapper apiMovieMapper;

        public ItemManager(IItemAPIRepository itemApiRepo, IItemRepository itemRepo) {
            this.itemApiRepo = itemApiRepo;
            this.itemRepo = itemRepo;

            apiMovieMapper = new APISeriesMapper();
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

            //Create the viewmodel
            IndexViewModel indexViewModel = new IndexViewModel { PopularItems = popularMoviesRoot.Results.Take(6) };

            return indexViewModel;
        }

        public IndexViewModel GetSeriesIndexViewModel() {
            IRestResponse popularSeriesResponse = itemApiRepo.GetPopularSeries();

            PopularSeriesRoot popularSeriesRoot = JsonConvert.DeserializeObject<PopularSeriesRoot>(popularSeriesResponse.Content);

            List<Series> series = new List<Series>();

            foreach (PopularSeriesResult result in popularSeriesRoot.Results) {
                series.Add(apiMovieMapper.mapMovie(result));
            }

            IndexViewModel indexVieModel = new IndexViewModel { PopularItems = series.Take(6) };

            return indexVieModel;
        }

        /// <author>Dennis van Hest</author>
        /// <summary>
        /// Returns the view model for the Movie detail page for the given movie id
        /// </summary>
        /// <param name="id">Id of the movie</param>
        /// <returns>The MovieViewModel</returns>
        public MovieViewModel GetMovieById(int id) {
            Movie movie = itemRepo.GetMovieById(id);

            //If the movie is not found in the database, get the movie from the API
            if (movie == null) {
                movie = GetMovieByIdFromAPI(id);
                itemRepo.InsertItem(movie);
            }

            IEnumerable<CastMember> castMembers = movie.ItemCastMembers.Select(ic => ic.CastMember).Distinct().Take(5);
            IEnumerable<Genre> genres = movie.ItemGenres.Select(ig => ig.Genre);
            IEnumerable<CrewMember> directors = movie.ItemCrewMembers.Where(ic => ic.Job == "Director").Select(ic => ic.CrewMember).Distinct();

            return new MovieViewModel {
                Title = movie.Title,
                Description = movie.Description,
                ReleaseDate = movie.ReleaseDate,
                PosterURL = movie.PosterPath,
                BackdropURL = movie.BackdropURL,
                Genres = genres,
                CastMembers = castMembers,
                Directors = directors,
                Subtitles = movie.Subtitles
            };
        }

        public SeriesViewModel GetSeriesById(int id) {
            Series series = itemRepo.GetSeriesById(id);

            //If the series is not found in the database, get the series from the API
            if (series == null) {
                series = GetSeriesByIdFromApi(id);
                itemRepo.InsertItem(series);
            }

            IEnumerable<CastMember> castMembers = series.ItemCastMembers.Select(ic => ic.CastMember).Distinct().Take(5);
            IEnumerable<Genre> genres = series.ItemGenres.Select(ig => ig.Genre);
            IEnumerable<CrewMember> directors = series.ItemCrewMembers.Where(ic => ic.Job == "Director").Select(ic => ic.CrewMember).Distinct();

            return new SeriesViewModel {
                Title = series.Title,
                Description = series.Description,
                ReleaseDate = series.ReleaseDate,
                PosterURL = series.PosterPath,
                BackdropURL = series.BackdropURL,
                Genres = genres,
                CastMembers = castMembers,
                Directors = directors
            };
        }

        /// <summary>
        /// Returns the movie with the given id from the API
        /// </summary>
        /// <param name="id">Id of the movie</param>
        /// <returns>A Movie object</returns>
        private Movie GetMovieByIdFromAPI(int id) {
            //Retrieve the movie from the API
            IRestResponse movieResponse = itemApiRepo.GetMovieById(id);

            //Convert the response to a movie object
            MovieApiResult apiMovie = JsonConvert.DeserializeObject<MovieApiResult>(movieResponse.Content);

            //Retrieve the credits (Person objects) from the API
            IRestResponse creditsResponse = itemApiRepo.GetCreditsByMovieId(id);

            //Convert the response to a MovieCreditsRoot object
            CreditsRoot movieCreditsRoot = JsonConvert.DeserializeObject<CreditsRoot>(creditsResponse.Content);

            Movie movie = new Movie {
                Id = apiMovie.Id,
                Title = apiMovie.Title,
                Description = apiMovie.Description,
                ReleaseDate = apiMovie.ReleaseDate,
                PosterPath = apiMovie.PosterURL,
                BackdropURL = apiMovie.BackdropURL
            };

            movie.ItemGenres = new List<ItemGenre>();

            foreach (Genre genre in apiMovie.Genres) {
                movie.ItemGenres.Add(new ItemGenre { ItemId = movie.Id, GenreId = genre.Id, Item = movie, Genre = genre });
            }

            //Add all people to the movie
            movie.ItemCrewMembers = new List<ItemCrewMember>();

            foreach (CrewMemberResult crewMember in movieCreditsRoot.Crew) {
                movie.ItemCrewMembers.Add(
                        new ItemCrewMember {
                            ItemId = movie.Id,
                            CrewMemberId = crewMember.Id,
                            Item = movie,
                            CrewMember = new CrewMember { Id = crewMember.Id, Name = crewMember.Name },
                            Job = crewMember.Job
                        }
                    );
            }

            movie.ItemCastMembers = new List<ItemCastMember>();

            foreach (CastMemberResult castMember in movieCreditsRoot.Cast) {
                movie.ItemCastMembers.Add(
                        new ItemCastMember {
                            ItemId = movie.Id,
                            CastMemberId = castMember.Id,
                            Item = movie,
                            CastMember = new CastMember { Id = castMember.Id, Name = castMember.Name },
                            Character = castMember.Character
                        }
                    );
            }

            return movie;
        }

        public Series GetSeriesByIdFromApi(int id) {
            IRestResponse seriesResponse = itemApiRepo.GetSeriesById(id);

            //Convert the response to a movie object
            SeriesApiResult apiSeries = JsonConvert.DeserializeObject<SeriesApiResult>(seriesResponse.Content);

            //Retrieve the credits (Person objects) from the API
            IRestResponse creditsResponse = itemApiRepo.GetCreditsBySeriesId(id);

            //Convert the response to a MovieCreditsRoot object
            CreditsRoot creditsRoot = JsonConvert.DeserializeObject<CreditsRoot>(creditsResponse.Content);

            Series series = new Series() {
                Id = apiSeries.Id,
                Title = apiSeries.Name,
                Description = apiSeries.Description,
                ReleaseDate = apiSeries.AirDate,
                PosterPath = apiSeries.PosterURL,
                BackdropURL = apiSeries.BackdropPath
            };

            series.ItemGenres = new List<ItemGenre>();

            foreach (Genre genre in apiSeries.Genres) {
                series.ItemGenres.Add(new ItemGenre { ItemId = series.Id, GenreId = genre.Id, Item = series, Genre = genre });
            }

            series.ItemCrewMembers = new List<ItemCrewMember>();

            foreach (CrewMemberResult crewMember in creditsRoot.Crew) {
                series.ItemCrewMembers.Add(
                        new ItemCrewMember {
                            ItemId = series.Id,
                            CrewMemberId = crewMember.Id,
                            Item = series,
                            CrewMember = new CrewMember { Id = crewMember.Id, Name = crewMember.Name },
                            Job = crewMember.Job
                        }
                    );
            }

            series.ItemCastMembers = new List<ItemCastMember>();

            foreach (CastMemberResult castMember in creditsRoot.Cast) {
                series.ItemCastMembers.Add(
                        new ItemCastMember {
                            ItemId = series.Id,
                            CastMemberId = castMember.Id,
                            Item = series,
                            CastMember = new CastMember { Id = castMember.Id, Name = castMember.Name },
                            Character = castMember.Character
                        }
                    );
            }

            series.Seasons = new List<Season>();

            for (int seasonNr = 1; seasonNr <= apiSeries.NumberOfSeasons; seasonNr++) {
                IRestResponse seasonResponse = itemApiRepo.GetSeriesSeasonById(id, seasonNr);

                //Convert the response to a movie object
                Season season = JsonConvert.DeserializeObject<Season>(seasonResponse.Content);

                series.Seasons.Add(season);
            }

            return series;
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
        public IEnumerable<ItemAPIModel> SearchMovies(string query) {
            List<ItemAPIModel> itemList = new List<ItemAPIModel>();

            IEnumerable<Movie> result = itemRepo.SearchMoviesByTitle(query);

            foreach (Movie movie in result) {
                ItemAPIModel itemApiModel = new ItemAPIModel {
                    Id = movie.Id,
                    Title = movie.Title,
                    PosterURL = movie.PosterPath
                };

                itemList.Add(itemApiModel);
            }

            return itemList;
        }

        public IEnumerable<SeriesAPIModel> SearchSeries(string query) {
            List<SeriesAPIModel> seriesList = new List<SeriesAPIModel>();

            IEnumerable<Series> result = itemRepo.SearchSeriesByTitle(query);

            foreach (Series series in result) {
                SeriesAPIModel seriesApiModel = new SeriesAPIModel {
                    Id = series.Id,
                    Title = series.Title,
                    PosterURL = series.PosterPath
                };

                seriesApiModel.Seasons = new List<SeasonAPIModel>();

                foreach (Season season in series.Seasons) {
                    SeasonAPIModel seasonApiModel = new SeasonAPIModel {
                        Id = season.Id,
                        SeasonNumber = season.SeasonNumber
                    };

                    seasonApiModel.Episodes = new List<EpisodeAPIModel>();

                    foreach (Episode episode in season.Episodes) {
                        EpisodeAPIModel episodeApiModel = new EpisodeAPIModel {
                            Id = episode.Id,
                            EpisodeNumber = episode.EpisodeNumber,
                            Name = episode.Name
                        };

                        seasonApiModel.Episodes.Add(episodeApiModel);
                    }

                    seriesApiModel.Seasons.Add(seasonApiModel);
                }

                seriesList.Add(seriesApiModel);
            }

            return seriesList;
        }
    }
}