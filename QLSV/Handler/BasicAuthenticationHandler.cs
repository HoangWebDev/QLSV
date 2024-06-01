using Azure.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using QLSV.Contracts;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text;

namespace QLSV.Handler
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserRepository _userRepository;
        public BasicAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    ISystemClock clock,
    IUserRepository userRepository) : base(options, logger, encoder, clock)
        {
            _userRepository = userRepository;
        }

        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var authorizationHeader = Context.Request.Headers["Authorization"].ToString();
            if (authorizationHeader != null && authorizationHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase))
            {
                var token = authorizationHeader.Substring("Basic ".Length).Trim();
                var credentialsAsEncodedString = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                var credentials = credentialsAsEncodedString.Split(':');
                string role = _userRepository.Authenticate(credentials[0], credentials[1]);
                if (role != null)
                {
                    var claims = new[] { new Claim("name", credentials[0]), new Claim(ClaimTypes.Role, role) };
                    var identity = new ClaimsIdentity(claims, "Basic");
                    var claimsPrincipal = new ClaimsPrincipal(identity);
                    //await Context.SignInAsync(claimsPrincipal);
                    return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name));
                }
            }
            Response.StatusCode = 401;
            Response.Headers.Add("WWW-Authenticate", "Basic realm=\"joydipkanjilal.com\"");
            return AuthenticateResult.Fail("Invalid Authorization Header");
        }
    }
}
