using System;
using System.Net;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Token.BRL.Interfaces;

namespace Token.API.Controllers
{
    [Route("api/token")]
    public class TokenController : BaseController, IController
    {

        private readonly ITokenService _tokenService;
        private readonly ILogger<TokenController> _logger;

        public TokenController(ITokenService tokenService, ILogger<TokenController> logger)
        {
            _tokenService = tokenService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> GetToken()
        {
            try
            {
                //get user login
                var identity = User.Identity as ClaimsIdentity;

                if (identity?.Name == null) throw new AuthenticationException("User.Identity is null");

                var response = await _tokenService.GenerateAccessToken(identity.Name);

                return Ok(response);
            }
            catch (Exception ex)
            {
                LogError(_logger, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}