using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QLSV.Contracts;
using QLSV.Model;
using QLSV.Repository;
using QLSV.Request;
using QLSV.Response;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QLSV.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserAPI : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;
        public UserAPI(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;

        }

        [HttpGet]
        [Authorize]
        public async Task<List<User>> GetUserNames()
        {
            return await _userRepository.GetUserNames();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Login")]

        public string Login([FromBody] LoginRequest userLogin)
        {
            var user = _userRepository.GetUser(userLogin.Username, userLogin.Password);
            if (user != null)
            {
                string token = GenerateToken(user);
                return token;
            }

            return "user not found";
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

           // var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Username),
                new Claim(ClaimTypes.Role,user.Role)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
