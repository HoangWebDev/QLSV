using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLSV.Contracts;
using QLSV.Model;
using QLSV.Repository;
using QLSV.Response;
using System.Collections.Generic;
using System.Data;

namespace QLSV.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet]
        [Authorize]
        public async Task<List<User>> GetUserNames()
        {
            return await _userRepository.GetUserNames();
        }
    }
}
