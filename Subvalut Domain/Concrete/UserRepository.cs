using Subvault_Domain.Abstract;
using System.Linq;
using Subvault_Domain.Entities;

namespace Subvault_Domain.Concrete {

    public class UserRepository : IUserRepository {

        private EFDbContext context;

        public UserRepository(EFDbContext context) {
            this.context = context;
        }

        public User GetUser(string username) {
            return context.Users
                .Where(u => u.Username == username)
                .FirstOrDefault();
        }

        public string GetHashedPassword(string username) {
            return context.Users
                .Where(u => u.Username == username)
                .Select(u => u.HashedPassword)
                .FirstOrDefault();
        }

        public string GetSalt(string username) {
            return context.Users
                .Where(u => u.Username == username)
                .Select(u => u.Salt)
                .FirstOrDefault();
        }

        public void CreateUser(User user) {
            context.Users.Add(user);
            context.SaveChanges();
        }

        public bool UserExists(string username) {
            return context.Users
                .Where(u => u.Username == username)
                .Count() != 0;
        }
    }
}
