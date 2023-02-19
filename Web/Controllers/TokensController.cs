using Core.Interfaces.Services;
using Core.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly ITokensService _tokensService;

        private Guid _userId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        public TokensController(ITokensService tokensService)
        {
            _tokensService = tokensService ?? throw new ArgumentNullException(nameof(tokensService));
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshAccessTokenAsync([FromBody] TokensRefreshingViewModel tokenViewModel)
        {
            var tokensPair = await _tokensService.RefreshAccessTokenAsync(tokenViewModel.RefreshToken.ToString(),
                tokenViewModel.AccessToken);

            return Ok(tokensPair);
        }
    }
}
