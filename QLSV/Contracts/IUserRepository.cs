using QLSV.Model;
using QLSV.Response;
using System.Collections.Generic;

namespace QLSV.Contracts
{
    public interface IUserRepository
    {
        Task<bool> Authenticate(string username, string password);
        Task<List<User>> GetUserNames();
    }
}
