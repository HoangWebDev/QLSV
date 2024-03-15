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
                Id = 1, Username = "peter", Password = "peter123"
            },
            new User
            {
                Id = 2, Username = "joydip", Password = "joydip123"
            },
            new User
            {
                Id = 3, Username = "james", Password = "james123"
            }
        };
        public async Task Authenticate(string username, string password)
        {
            if (await Task.FromResult(_users.SingleOrDefault(x => x.Username == username && x.Password == password)) != null)
            {
                return true;
            }
            return false;
        }
        public async Task<List<User>> GetUserNames()
        {
            List users = new List();
            foreach (var user in _users)
            {
                users.Add(user.Username);
            }
            return await Task.FromResult(users);
        }
    }
}
