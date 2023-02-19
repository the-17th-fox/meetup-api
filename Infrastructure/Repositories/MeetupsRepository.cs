
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Utilities;
using Core.ViewModels.Meetups;
using Core.ViewModels.Pagination;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MeetupsRepository : IMeetupsRepository
    {
        private readonly MeetupsDbContext _context;

        public MeetupsRepository(MeetupsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateAsync(Meetup meetup)
        {
            _context.Meetups.Add(meetup);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Meetup meetup)
        {
            _context.Meetups.Remove(meetup);
            await _context.SaveChangesAsync();
        }

        public async Task<PagedList<Meetup>> GetAllAsync(PageParametersViewModel pageParameters)
        {
            var meetups = _context.Meetups.AsNoTracking();
            return await PagedList<Meetup>.ToPagedListAsync(meetups, pageParameters.PageNumber, pageParameters.PageSize);
        }

        public async Task<Meetup?> GetByTimeAsync(DateTime time)
        {
            return await _context.Meetups
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.StartsAt == time);
        }

        public async Task<Meetup?> GetByIdAsync(Guid id, bool asNoTracking = false)
        {
            return asNoTracking ?
                await _context.Meetups.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id)
                : await _context.Meetups.FindAsync(id);
        }

        public async Task UpdateAsync(Meetup meetup, Meetup newMeetup)
        {
            _context.Update(meetup);
            meetup.MeetupManager = newMeetup.MeetupManager;
            meetup.Speaker = newMeetup.Speaker;
            meetup.Title = newMeetup.Title;
            meetup.Description = newMeetup.Description;
            meetup.Location = newMeetup.Location;
            meetup.StartsAt = newMeetup.StartsAt;
            
            await _context.SaveChangesAsync();
        }
    }
}
