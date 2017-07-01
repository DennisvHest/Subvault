using System.ComponentModel.DataAnnotations;

namespace Subvault_Domain.Entities {

    public class User {

        [Key]
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
        public string EmailAdress { get; set; }
    }
}
