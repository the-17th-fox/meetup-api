using AutoMapper;
using Core.Constants;
using Core.Interfaces.Services;
using Core.Models;
using Core.ViewModels.Meetups;
using Core.ViewModels.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Security.Claims;

namespace Web.Controllers
{
    [Authorize]
    [Route("api/meetups")]
    [ApiController]
    public class MeetupsController : ControllerBase
    {
        private readonly IMeetupsService _meetupsService;
        private readonly IMapper _mapper;
        private readonly ILogger<MeetupsController> _logger;

        private Guid _userId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        private string _userEmail => User.FindFirstValue(ClaimTypes.Email);

        public MeetupsController(IMeetupsService meetupsService, IMapper mapper, ILogger<MeetupsController> logger)
        {
            _meetupsService = meetupsService ?? throw new ArgumentNullException(nameof(meetupsService)) ;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            _logger.LogInformation(LogEvents.MeetupRetrievingAttemptById, _userEmail, _userId, id);

            var meetup = await _meetupsService.GetByIdAsync(id);
            var meetupModel = _mapper.Map<MeetupViewModel>(meetup);

            _logger.LogInformation(LogEvents.MeetupRetrievingByIdSucceeded, _userEmail, _userId, id);
            return Ok(meetupModel);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync([FromQuery] PageParametersViewModel pageParameters)
        {
            _logger.LogInformation(LogEvents.PagedMeetupRetrievingAttempt, _userEmail, _userId, pageParameters.PageNumber, pageParameters.PageSize);

            var meetups = await _meetupsService.GetAllAsync(pageParameters);
            var meetupsModels = _mapper.Map<PageViewModel<ShortMeetupViewModel>>(meetups);

            _logger.LogInformation(LogEvents.PagedMeetupRetrievingSucceeded, _userEmail, _userId, pageParameters.PageNumber, pageParameters.PageSize);
            return Ok(meetupsModels);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync(CreateMeetupViewModel newMeetup)
        {
            _logger.LogInformation(LogEvents.MeetupCreationAttempt, _userEmail, _userId, newMeetup);

            var meetup = _mapper.Map<Meetup>(newMeetup);
            await _meetupsService.CreateAsync(meetup);

            _logger.LogInformation(LogEvents.MeetupCreationSucceeded, _userEmail, _userId, newMeetup);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            _logger.LogInformation(LogEvents.MeetupDeletionAttempt, _userEmail, _userId, id);

            await _meetupsService.DeleteAsync(id);

            _logger.LogInformation(LogEvents.MeetupDeletionSucceeded, _userEmail, _userId, id);
            return Ok();
        }

        [HttpPatch("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateMeetupViewModel updateMeetupViewModel)
        {
            _logger.LogInformation(LogEvents.MeetupUpdatingAttempt, _userEmail, _userId, updateMeetupViewModel.Id, updateMeetupViewModel);

            var updatedMeetup = _mapper.Map<Meetup>(updateMeetupViewModel);
            await _meetupsService.UpdateAsync(updatedMeetup);

            _logger.LogInformation(LogEvents.MeetupUpdatingSucceded, _userEmail, _userId, updateMeetupViewModel.Id, updateMeetupViewModel);
            return Ok();
        }
    }
}
