using AutoMapper;
using Core.Constants.CustomExceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Utilities;
using Core.ViewModels.Meetups;
using Core.ViewModels.Pagination;

namespace Core.Services
{
    public class MeetupsService : IMeetupsService
    {
        private readonly IMeetupsRepository _meetupsRepository;

        public MeetupsService(IMeetupsRepository meetupsRepository)
        {
            _meetupsRepository = meetupsRepository ?? throw new ArgumentNullException(nameof(meetupsRepository));
        }

        public async Task CreateAsync(Meetup meetup)
        {
            var existingMeetup = await _meetupsRepository.GetByTimeAsync(meetup.StartsAt);
            if (existingMeetup != null)
                throw new BadRequestException("There is already a meetup at the specified time");

            await _meetupsRepository.CreateAsync(meetup);
        }

        public async Task DeleteAsync(Guid id)
        {
            var meetup = await CheckIfExistsAsync(id); 

            await _meetupsRepository.DeleteAsync(meetup);
        }

        public async Task UpdateAsync(Meetup updatedMeetup)
        {
            var meetup = await CheckIfExistsAsync(updatedMeetup.Id);

            await _meetupsRepository.UpdateAsync(meetup, updatedMeetup);
        }

        private async Task<Meetup> CheckIfExistsAsync(Guid id, bool asNoTracking = false)
            => await _meetupsRepository.GetByIdAsync(id, asNoTracking) ?? throw new NotFoundException("Meetup with the specified id hasn't been found");

        public async Task<PagedList<Meetup>> GetAllAsync(PageParametersViewModel pageParameters)
            => await _meetupsRepository.GetAllAsync(pageParameters);

        public async Task<Meetup> GetByIdAsync(Guid id)
            => await CheckIfExistsAsync(id, asNoTracking: true);
    }
}
