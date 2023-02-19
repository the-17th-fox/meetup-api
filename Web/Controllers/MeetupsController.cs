using AutoMapper;
using Core.Interfaces.Services;
using Core.Models;
using Core.ViewModels.Meetups;
using Core.ViewModels.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Web.Controllers
{
    [Authorize]
    [Route("api/meetups")]
    [ApiController]
    public class MeetupsController : ControllerBase
    {
        private readonly IMeetupsService _meetupsService;
        private readonly IMapper _mapper;

        public MeetupsController(IMeetupsService meetupsService, IMapper mapper)
        {
            _meetupsService = meetupsService ?? throw new ArgumentNullException(nameof(meetupsService)) ;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var meetup = await _meetupsService.GetByIdAsync(id);
            var meetupModel = _mapper.Map<MeetupViewModel>(meetup);
            return Ok(meetupModel);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync([FromQuery] PageParametersViewModel pageParameters)
        {
            var meetups = await _meetupsService.GetAllAsync(pageParameters);
            var meetupsModels = _mapper.Map<List<ShortMeetupViewModel>>(meetups);
            return Ok(meetupsModels);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync(CreateMeetupViewModel newMeetup)
        {
            var meetup = _mapper.Map<Meetup>(newMeetup);
            await _meetupsService.CreateAsync(meetup);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _meetupsService.DeleteAsync(id);
            return Ok();
        }

        [HttpPatch("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateMeetupViewModel updateMeetupViewModel)
        {
            var updatedMeetup = _mapper.Map<Meetup>(updateMeetupViewModel);
            await _meetupsService.UpdateAsync(updatedMeetup);
            return Ok();
        }
    }
}
