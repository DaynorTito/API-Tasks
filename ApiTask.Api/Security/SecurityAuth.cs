using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ApiTask.Domain.Ports;

namespace ApiTask.Api.Security
{
    public class SecurityAuth : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserPersistencePort _userPersistenceAdapter;

        public SecurityAuth(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IUserPersistencePort userPersistenceAdapter
        )
            : base(options, logger, encoder, clock)
        {
            _userPersistenceAdapter = userPersistenceAdapter;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                if (!Request.Headers.ContainsKey("Authorization"))
                {
                    return AuthenticateResult.Fail("Missing Authorization Header");
                }
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter)).Split(':');
                var email = credentials[0];
                var password = credentials[1];
                var user = await _userPersistenceAdapter.GetUserByEmailAsync(email);
                if (user == null || user.Passwd != password)
                {
                    return AuthenticateResult.Fail("Invalid Email or Password");
                }
                else
                {
                    var claims = new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                        new Claim(ClaimTypes.Name, user.Email)
                    };

                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);

                    return AuthenticateResult.Success(ticket);
                }
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail("error "+ex.Message);
            }
        }
    }
}
