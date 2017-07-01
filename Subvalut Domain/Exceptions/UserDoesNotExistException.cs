using System;

namespace Subvault_Domain.Exceptions {

    public class UserDoesNotExistException : Exception {

        public UserDoesNotExistException(string message) : base(message) { }
    }
}
