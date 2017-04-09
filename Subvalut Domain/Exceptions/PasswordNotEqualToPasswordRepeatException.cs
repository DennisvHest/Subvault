using System;

namespace Subvault_Domain.Exceptions {

    public class PasswordNotEqualToPasswordRepeatException : Exception {

        public PasswordNotEqualToPasswordRepeatException(string message) : base(message) { }
    }
}
