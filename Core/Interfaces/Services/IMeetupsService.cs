using Core.Models;
using Core.Utilities;
using Core.ViewModels.Meetups;
using Core.ViewModels.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IMeetupsService
    {
        public Task CreateAsync(Meetup meetup);
        public Task<Meetup> GetByIdAsync(Guid id);
        public Task<PagedList<Meetup>> GetAllAsync(PageParametersViewModel pageParameters);
        public Task UpdateAsync(Meetup updatedMeetup);
        public Task DeleteAsync(Guid id);
    }
}
