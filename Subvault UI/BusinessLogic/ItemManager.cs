﻿using Newtonsoft.Json;
using RestSharp;
using Subvalut_Domain.APIEntities;
using Subvault_Domain;
using Subvault_Domain.Abstract;
using Subvault_Domain.Concrete;
using Subvault_Domain.Entities;
using Subvault_Domain.ObjectMappers;
using Subvault_UI.Models;
using System;
using System.Collections;
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

        public MovieViewModel GetMovieById(int id) {
            Movie movie = itemRepo.GetMovieById(id);

            return new MovieViewModel {
                Title = movie.Title,
                Description = movie.Description,
                ReleaseDate = movie.ReleaseDate,
                PosterURL = movie.PosterURL,
                BackdropURL = movie.BackdropURL,
                Genres = movie.Genres
            };
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