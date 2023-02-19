using AutoMapper;
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
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersController> _logger;

        private Guid _userId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        private string _userEmail => User.FindFirstValue(ClaimTypes.Email);

        public UsersController(IUsersService usersService, IMapper mapper, ILogger<UsersController> logger)
        {
            _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel loginModel)
        {
            _logger.LogInformation(LogEvents.LoginAttempt, loginModel.Email);

            var tokensPair = await _usersService.LoginAsync(loginModel);

            _logger.LogInformation(LogEvents.TokenReturned, loginModel.Email);

            return Ok(tokensPair);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            _logger.LogInformation(LogEvents.LogOutAttempt, _userEmail);

            await _usersService.LogoutAsync(_userId);

            _logger.LogInformation(LogEvents.LogOutSucceeded, _userEmail);

            return Ok();
        }
    }
}
