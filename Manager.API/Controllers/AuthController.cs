using Manager.API.Auth;
using Manager.Domain.Commands.AuthCommands;
using Manager.Domain.Contracts.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;

namespace Manager.API.Controllers
{
    [Route("api/security/token")]
    public class AuthController : BaseController
    {
        private readonly TokenConfigurations _tokenConfigurations;
        private readonly IUserRepository _userRepository;

        public AuthController(
            TokenConfigurations tokenConfigurations,
            IUserRepository userRepository)
        {
            _tokenConfigurations = tokenConfigurations;
            _userRepository = userRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromForm] AuthCommand command)
        {
            var user = _userRepository.Authenticate(command.Username, command.Password);

            if (user == null)
                return BadRequest("Usuário ou senha inválido");

            var identity = new ClaimsIdentity(new GenericIdentity(user.Vendor.Name, "Login"));

            identity.AddClaim(new Claim(ClaimTypes.Sid, user.VendorId.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Email, user.Username));
            identity.AddClaim(new Claim(ClaimTypes.GivenName, user.Vendor.Name));
            identity.AddClaim(new Claim(ClaimTypes.Role, user.IsAdmin ? "admin" : ""));

            var principal = new GenericPrincipal(identity, new string[] { user.IsAdmin ? "admin" : "" });
            Thread.CurrentPrincipal = principal;

            var (access_token, expiration) = TokenManager.Create(identity, _tokenConfigurations);
            return Ok(new
            {
                user.Vendor.Name,
                access_token,
                expiration
            });
        }
    }
}
