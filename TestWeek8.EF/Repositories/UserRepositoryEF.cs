using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWeek8.Core.Interfaces;
using TestWeek8.Core.Models;

namespace TestWeek8.EF.Repositories
{
    public class UserRepositoryEF : IUserRepository
    {
        private readonly RestaurantContext ctx;
        public UserRepositoryEF(RestaurantContext context)
        {
            ctx = context;
        }
        public bool AddItem(User item)
        {
            throw new NotImplementedException();
        }

        public bool DeleteItemById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> Fetch(Func<User, bool> filter = null)
        {
            throw new NotImplementedException();
        }

        public User GetByEmail(string email)
        {

            if (string.IsNullOrEmpty(email))
                return null;
            return ctx.Users.SingleOrDefault(u => u.Email.Equals(email));
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateItem(User updatedItem)
        {
            throw new NotImplementedException();
        }
    }
}
