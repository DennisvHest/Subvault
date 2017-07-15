using Subvault_Domain.Abstract;
using System.Linq;
using Subvault_Domain.Entities;

namespace Subvault_Domain.Concrete {

    public class UserRepository : IUserRepository {

        private EFDbContext context;

        public UserRepository(EFDbContext context) {
            this.context = context;
        }

        /// <author>Dennis van Hest</author>
        /// <summary>
        /// Returns the user with the given username
        /// </summary>
        /// <param name="username">The username of the user</param>
        /// <returns>The User object</returns>
        public User GetUser(string username) {
            Logger.Log.InfoFormat(Logger.Format + "Getting user with username: " + username + " from the database", GetType().ToString());

            return context.Users
                .Where(u => u.Username == username)
                .FirstOrDefault();
        }

        /// <author>Dennis van Hest</author>
        /// <summary>
        /// Returns the hashed password of the user with the given username
        /// </summary>
        /// <param name="username">The username of the user</param>
        /// <returns>The hashed password</returns>
        public string GetHashedPassword(string username) {
            Logger.Log.InfoFormat(Logger.Format + "Getting the hashed password for the user with username: " + username + " from the database", GetType().ToString());

            return context.Users
                .Where(u => u.Username == username)
                .Select(u => u.HashedPassword)
                .FirstOrDefault();
        }

        /// <author>Dennis van Hest</author>
        /// <summary>
        /// Returns the salt of the user with the given username
        /// </summary>
        /// <param name="username">The username of the user</param>
        /// <returns>The salt</returns>
        public string GetSalt(string username) {
            Logger.Log.InfoFormat(Logger.Format + "Getting the salt from the user with username: " + username + " from the database", GetType().ToString());

            return context.Users
                .Where(u => u.Username == username)
                .Select(u => u.Salt)
                .FirstOrDefault();
        }

        /// <summary>
        /// Inserts the given user into the database
        /// </summary>
        /// <param name="user">The user</param>
        public void CreateUser(User user) {
            Logger.Log.InfoFormat(Logger.Format + "Adding user with username: " + user.Username + " to the database", GetType().ToString());

            context.Users.Add(user);
            context.SaveChanges();
        }

        /// <author>Dennis van Hest</author>
        /// <summary>
        /// Checks if a user exists or not
        /// </summary>
        /// <param name="username">The username of the user</param>
        /// <returns>A boolean value (true = user exists, false = user does not exist)</returns>
        public bool UserExists(string username) {
            Logger.Log.InfoFormat(Logger.Format + "Checking if user with username: " + username + " exists in the database", GetType().ToString());

            return context.Users
                .Where(u => u.Username == username)
                .Count() != 0;
        }
    }
}
