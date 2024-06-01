using Dapper;
using QLSV.Context;
using QLSV.Contracts;
using QLSV.Model;
using QLSV.Response;
using System.Collections.Generic;
using System.Data;

namespace QLSV.Repository
{
    public class UserRepository : IUserRepository
    {
        private List<User> _users = new List<User>()
        {
            new User
            {
                Id = 1, Username = "peter", Password = "peter123", Role = "Admin"
            },
            new User
            {
                Id = 2, Username = "joydip", Password = "joydip123"
            },
            new User
            {
                Id = 3, Username = "james", Password = "james123", Role = "User"
            }
        };
        public string Authenticate(string username, string password)
        {

            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);
            if (user != null) return user.Role;
            return null;
        }

        public User GetUser(string username, string password)
        {

            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);
            return user;
        }
        public async Task<List<User>> GetUserNames()
        {
            List<User> users = new List<User>();
            foreach (var user in _users)
            {
                users.Add(new User { Username = user.Username });
            }
            return await Task.FromResult(users);
        }
    }
}
