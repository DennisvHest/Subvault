using System;

namespace Subvault_Domain.Exceptions {

    public class UserAlreadyExistsException : Exception {

        public UserAlreadyExistsException(string message) : base(message) { }
    }
}
