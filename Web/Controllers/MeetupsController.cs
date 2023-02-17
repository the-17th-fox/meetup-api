using Core.Interfaces;
using Core.ViewModels.Meetups;
using Core.ViewModels.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Web.Controllers
{
    [Route("api/meetups")]
    [ApiController]
    public class MeetupsController : ControllerBase
    {
        private readonly IMeetupsService _meetupsService;

        public MeetupsController(IMeetupsService meetupsService)
        {
            _meetupsService = meetupsService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            return Ok(await _meetupsService.GetByIdAsync(id));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync([FromQuery] PageParametersViewModel pageParameters)
        {
            return Ok(await _meetupsService.GetAllAsync(pageParameters));
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync(CreateMeetupViewModel newMeetup)
        {
            await _meetupsService.CreateAsync(newMeetup);
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
            await _meetupsService.UpdateAsync(updateMeetupViewModel);
            return Ok();
        }
    }
}
