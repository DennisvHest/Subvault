using Subvault_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subvault_Domain.Abstract {
    public interface IUserRepository {

        User GetUser(string username);
        string GetHashedPassword(string username);
        string GetSalt(string username);
        void CreateUser(User user);
        bool UserExists(string username);
    }
}
