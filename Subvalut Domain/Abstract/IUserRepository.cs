using Subvault_Domain.Entities;

namespace Subvault_Domain.Abstract {
    public interface IUserRepository {

        User GetUser(string username);
        string GetHashedPassword(string username);
        string GetSalt(string username);
        void CreateUser(User user);
        bool UserExists(string username);
    }
}
