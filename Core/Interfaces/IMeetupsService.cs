using Core.Models;
using Core.Utilities;
using Core.ViewModels.Meetups;
using Core.ViewModels.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IMeetupsService
    {
        public Task CreateAsync(CreateMeetupViewModel meetupModel);
        public Task<Meetup> GetByIdAsync(Guid id);
        public Task<PagedList<Meetup>> GetAllAsync(PageParametersViewModel pageParameters);
        public Task UpdateAsync(UpdateMeetupViewModel updateMeetupViewModel);
        public Task DeleteAsync(Guid id);
    }
}
