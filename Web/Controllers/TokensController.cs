using Core.Interfaces.Services;
using Core.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Core.Constants;

namespace Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly ITokensService _tokensService;
        private readonly ILogger<TokensController> _logger;

        private Guid _userId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        private string _userEmail => User.FindFirstValue(ClaimTypes.Email);

        public TokensController(ITokensService tokensService, ILogger<TokensController> logger)
        {
            _tokensService = tokensService ?? throw new ArgumentNullException(nameof(tokensService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshAccessTokenAsync([FromBody] TokensRefreshingViewModel tokenViewModel)
        {
            _logger.LogInformation(LogEvents.TokenRefreshingAttempt, _userEmail, _userId);

            var tokensPair = await _tokensService.RefreshAccessTokenAsync(tokenViewModel.RefreshToken.ToString(),
                tokenViewModel.AccessToken);

            _logger.LogInformation(LogEvents.TokenRefreshingSucceeded, _userEmail, _userId);

            return Ok(tokensPair);
        }
    }
}
