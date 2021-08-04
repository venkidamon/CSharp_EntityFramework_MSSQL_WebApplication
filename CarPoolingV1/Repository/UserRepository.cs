using CarPool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarPool.Repository
{
    public class UserRepository : IUserRepository
    {
        private RideDbContext db;
        public UserRepository(RideDbContext context)
        {
            this.db = context;
        }
        public int CreateUser(User user)
        {
            if (!UserExists(user))
            {
                
                user.Password=user.Password;
                db.Users.Add(user);
                db.SaveChanges();
                return 1;
            }
            return -1;

        }
        public bool Validate(User user)
        {
            
            User userValue = db.Users.Find(user.Email);
            if (userValue == null)
            {
                return false;
            }
            if (user.Password == userValue.Password)
            {
                return true;
            }
            return false;
        }
        public bool UserExists(User user)
        {
            User userValue = db.Users.Find(user.Email);
            if (userValue != null)
            {
                return true;
            }
            return false;
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}