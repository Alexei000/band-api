using AutoMapper;
using BandApi.Entities;
using BandApi.Helpers;
using BandApi.Models;

namespace BandApi.Profiles
{
    // ReSharper disable once UnusedMember.Global
    public class BandsProfile : Profile
    {
        public BandsProfile()
        {
            CreateMap<Band, BandDto>()
                .ForMember(dest => dest.FoundedYearsAgo, 
                    opt => opt.MapFrom(src => $"{src.Founded:yyyy} ({src.Founded.GetYearsAgo()})"));

        }
    }
}
