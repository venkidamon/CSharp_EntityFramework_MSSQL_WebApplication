using CarPool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Repository
{
    public interface IUserRepository
    {
        int CreateUser(User user);
        bool UserExists(User user);
        bool Validate(User user);
        void Dispose();
    }
}
