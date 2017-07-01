using Subvault_Domain.Abstract;
using Subvault_Domain.Entities;
using Subvault_Domain.Exceptions;
using Subvault_UI.Models;
using System.Web.Helpers;

namespace Subvault_UI.BusinessLogic {

    public class UserManager {

        private IUserRepository userRepository;

        public UserManager(IUserRepository userRepository) {
            this.userRepository = userRepository;
        }

        /// <author>Dennis van Hest</author>
        /// <summary>
        /// Authenticates the user with the given username and password
        /// </summary>
        /// <param name="username">The username of the user</param>
        /// <param name="password">The password of the user</param>
        /// <returns>A boolean value (true = correct username/password, false = incorrect username/password)</returns>
        public bool AuthenticateUser(string username, string password) {
            //Check if the user exists or not
            if (!userRepository.UserExists(username)) {
                throw new UserDoesNotExistException("A user with that username does not exist.");
            }

            //Get the salt and the hashed password belonging to the username
            string dbHashedPassword = userRepository.GetHashedPassword(username);
            string dbSalt = userRepository.GetSalt(username);

            //Verify the password
            return Crypto.VerifyHashedPassword(dbHashedPassword, password + dbSalt);
        }

        /// <author>Dennis van Hest</author>
        /// <summary>
        /// Creates a view model for the user session from the user with the given username
        /// </summary>
        /// <param name="username">The username of the user</param>
        /// <returns>The UserSessionViewModel</returns>
        public UserSessionViewModel GetUserSession(string username) {
            User user = userRepository.GetUser(username);
            return new UserSessionViewModel {
                Username = user.Username
            };
        }

        /// <author>Dennis van Hest</author>
        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="registeringUser">The user that will be registered</param>
        /// <throws>UserAlreadyExistsException if the user with the given username already exists</throws>
        /// <throws>PasswordNotEqualToPasswordRepeatException if the fiven password does not match the password repeat</throws>
        public void RegisterUser(RegisteringUser registeringUser) {
            //Check if a user with that username doesn't already exist
            if (userRepository.UserExists(registeringUser.Username)) {
                throw new UserAlreadyExistsException("A user with that username already exists.");
            }

            //Check if the Password is the same as the PasswordRepeat
            if (registeringUser.Password != registeringUser.PasswordRepeat) {
                throw new PasswordNotEqualToPasswordRepeatException("Your password repeat was not the same as your password. Please try again.");
            }

            //Hash and salt the password
            string salt = Crypto.GenerateSalt();
            string hashedPassword = HashPassword(registeringUser.Password, salt);

            //Create a new user
            User newUser = new User {
                Username = registeringUser.Username,
                HashedPassword = hashedPassword,
                Salt = salt,
                EmailAdress = registeringUser.EmailAddress
            };

            //Add the new user to the database
            userRepository.CreateUser(newUser);
        }

        /// <author>Dennis van Hest</author>
        /// <summary>
        /// Hashes a plain password using the given salt
        /// </summary>
        /// <param name="plainPassword">Password to be hashed</param>
        /// <param name="salt">Salt for hashing</param>
        /// <returns>The hashed password</returns>
        private string HashPassword(string plainPassword, string salt) {
            //Hash the password
            string saltedPassword = plainPassword + salt;

            return Crypto.HashPassword(saltedPassword);
        }
    }
}