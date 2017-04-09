using System.ComponentModel.DataAnnotations;

namespace Subvault_UI.Models {

    public class RegisteringUser {

        [Required(ErrorMessage = "Please enter a username.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter a password.")]
        [StringLength(50, ErrorMessage = "A password must contain between 8 and 50 characters.", ErrorMessageResourceName = null, ErrorMessageResourceType = null, MinimumLength = 8)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please repeat your password.")]
        public string PasswordRepeat { get; set; }

        [Required(ErrorMessage = "Please enter an email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string EmailAddress { get; set; }
    }
}