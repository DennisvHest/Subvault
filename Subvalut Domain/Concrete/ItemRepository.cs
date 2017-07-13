using Subvault_Domain.Abstract;
using System.Collections.Generic;
using System.Linq;
using Subvault_Domain.Entities;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace Subvault_Domain.Concrete {
    public class ItemRepository : IItemRepository {

        private EFDbContext context;

        public ItemRepository(EFDbContext context) {
            this.context = context;
        }

        /// <summary>
        /// Returns a Movie object with a given id
        /// </summary>
        /// <param name="id">The id of the movie</param>
        /// <returns>A Movie with the given id</returns>
        public Movie GetMovieById(int id) {
            return (Movie)context.Items
                .Where(m => m.Id == id).FirstOrDefault();
        }

        public Series GetSeriesById(int id) {
            return (Series)context.Items
                .Where(s => s.Id == id).FirstOrDefault();
        }

        /// <author>Dennis van Hest</author>
        /// <summary>
        /// Returns a list of items that contain the title string
        /// </summary>
        /// <param name="title">Title of the item</param>
        /// <returns>List of Items</returns>
        public IEnumerable<Movie> SearchMoviesByTitle(string title) {
            return context.Movies
                .Where(i => i.Title.Contains(title))
                .ToList();
        }

        public IEnumerable<Series> SearchSeriesByTitle(string title) {
            return context.Series
                .Where(s => s.Title.Contains(title))
                .ToList();
        }

        /// <summary>
        /// Adds the given item to the database removing any existing related objects from the item
        /// </summary>
        /// <param name="item">Item to be inserted into the database</param>
        public void InsertItem(Item item) {
            //Remove existing Genres
            List<ItemGenre> existingGenres = context.ItemGenres.Include(ig => ig.Genre).ToList();

            foreach (ItemGenre itemGenre in item.ItemGenres) {
                if (existingGenres.Where(g => g.Genre.Id == itemGenre.Genre.Id).Count() != 0) {
                    itemGenre.Genre = null;
                }
            }

            //Remove existing CrewMembers
            List<ItemCrewMember> existingCrewMembers = context.ItemCrewMembers.Include(ic => ic.CrewMember).ToList();

            foreach (ItemCrewMember itemCrewMember in item.ItemCrewMembers) {
                if (existingCrewMembers.Where(cm => cm.CrewMember.Id == itemCrewMember.CrewMember.Id).Count() != 0) {
                    itemCrewMember.CrewMember = null;
                } else {
                    existingCrewMembers.Add(itemCrewMember);
                }
            }

            //Remove existing CastMembers
            List<ItemCastMember> existingCastMembers = context.ItemCastMembers.Include(ic => ic.CastMember).ToList();

            foreach (ItemCastMember itemCastMember in item.ItemCastMembers) {
                if (existingCastMembers.Where(cm => cm.CastMember.Id == itemCastMember.CastMember.Id).Count() != 0) {
                    itemCastMember.CastMember = null;
                } else {
                    existingCastMembers.Add(itemCastMember);
                }
            }

            context.Items.Add(item);
            try {
                context.SaveChanges();
            } catch (DbEntityValidationException dbEx) {
                foreach (var validationErrors in dbEx.EntityValidationErrors) {
                    foreach (var validationError in validationErrors.ValidationErrors) {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }
        }
    }
}
