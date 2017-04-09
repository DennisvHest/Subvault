using Subvault_Domain.Abstract;
using Subvault_Domain.Entities;
using Subvault_Domain.Exceptions;
using Subvault_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace Subvault_UI.BusinessLogic {

    public class UserManager {

        private IUserRepository userRepository;

        public UserManager(IUserRepository userRepository) {
            this.userRepository = userRepository;
        }

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

        public UserSessionViewModel GetUserSession(string username) {
            User user = userRepository.GetUser(username);
            return new UserSessionViewModel {
                Username = user.Username
            };
        }

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

        private string HashPassword(string plainPassword, string salt) {
            //Hash the password
            string saltedPassword = plainPassword + salt;

            return Crypto.HashPassword(saltedPassword);
        }
    }
}