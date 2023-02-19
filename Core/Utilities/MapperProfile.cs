using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.Models;
using Core.ViewModels.Meetups;
using Core.ViewModels.Pagination;

namespace Core.Utilities
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateMeetupViewModel, Meetup>();
            CreateMap<UpdateMeetupViewModel, Meetup>();
            CreateMap<Meetup, MeetupViewModel>();
            CreateMap<Meetup, ShortMeetupViewModel>();

            CreateMap<PagedList<Meetup>, PageViewModel<ShortMeetupViewModel>>()
                .ForMember(i => i.Items, p => p.MapFrom(u => u.ToList()));
        }
    }
}
