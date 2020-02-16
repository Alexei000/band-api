using AutoMapper;
using BandApi.Entities;
using BandApi.Models;

namespace BandApi.Profiles
{
    // ReSharper disable once UnusedMember.Global
    public class AlbumsProfile : Profile
    {
        public AlbumsProfile()
        {
            CreateMap<Album, AlbumDto>().ReverseMap();
            CreateMap<AlbumForCreateDto, Album>();
            CreateMap<AlbumForUpdateDto, Album>().ReverseMap();
        }
    }
}
