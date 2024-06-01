using QLSV.Model;
using QLSV.Response;
using System.Collections.Generic;

namespace QLSV.Contracts
{
    public interface IUserRepository
    {
        string Authenticate(string username, string password);
        User GetUser(string username, string password);

        Task<List<User>> GetUserNames();
    }
}
