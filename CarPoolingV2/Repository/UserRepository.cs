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
                
                
                db.Users.Add(user);
                db.SaveChanges();
                return 1;
            }
            return -1;
        }
        public bool Validate(User user)
        {

            if (UserExists(user))
            {

                User users = db.Users.Find(user.Email);
                if (user.Password ==users.Password)
                    return true;
                return false;
            }
            return false;
        }

        public bool UserExists(User user)
        {
            User user1 = db.Users.Find(user.Email);
            
            if (user1 != null)
                return true;
            return false;
        }

        public void Save()
        {
            db.SaveChanges();
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}