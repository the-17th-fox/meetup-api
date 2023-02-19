using Core.Models;
using Core.Utilities;
using Core.ViewModels.Meetups;
using Core.ViewModels.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface IMeetupsRepository
    {
        public Task CreateAsync(Meetup meetup);
        public Task<Meetup?> GetByIdAsync(Guid id, bool asNoTracking = false);
        public Task<PagedList<Meetup>> GetAllAsync(PageParametersViewModel pageParameters);
        public Task<Meetup?> GetByTimeAsync(DateTime time);
        public Task UpdateAsync(Meetup meetup, Meetup newMeetup);
        public Task DeleteAsync(Meetup meetup);
    }
}
