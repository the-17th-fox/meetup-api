using Core.Interfaces;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Utilities;
using Core.ViewModels.Meetups;
using Core.ViewModels.Pagination;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class MeetupsService : IMeetupsService
    {
        private readonly IMeetupsRepository _meetupsRepository;

        public MeetupsService(IMeetupsRepository meetupsRepository)
        {
            _meetupsRepository = meetupsRepository ?? throw new ArgumentNullException(nameof(meetupsRepository));
        }

        public async Task CreateAsync(CreateMeetupViewModel meetupModel)
        {

            Meetup meetup = new()
            {
                Title = meetupModel.Title,
                Description = meetupModel.Description,
                Location = meetupModel.Location,
                StartsAt = meetupModel.StartsAt,
                MeetupManager = meetupModel.MeetupManager,
                Speaker = meetupModel.Speaker
            };

            await _meetupsRepository.CreateAsync(meetup);
        }

        public async Task DeleteAsync(Guid id)
        {
            var meetup = await CheckIfExistsAsync(id); 

            await _meetupsRepository.DeleteAsync(meetup);
        }

        public async Task UpdateAsync(UpdateMeetupViewModel updateMeetupViewModel)
        {
            var meetup = await CheckIfExistsAsync(updateMeetupViewModel.Id);

            await _meetupsRepository.UpdateAsync(meetup, updateMeetupViewModel);
        }

        private async Task<Meetup> CheckIfExistsAsync(Guid id, bool asNoTracking = false)
            => await _meetupsRepository.GetByIdAsync(id, asNoTracking) ?? throw new ArgumentNullException("meetup"); // temp exception naming

        public async Task<PagedList<Meetup>> GetAllAsync(PageParametersViewModel pageParameters)
            => await _meetupsRepository.GetAllAsync(pageParameters);

        public async Task<Meetup> GetByIdAsync(Guid id)
            => await CheckIfExistsAsync(id, asNoTracking: true);
    }
}
