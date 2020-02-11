using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BandApi.Entities;
using BandApi.Models;

namespace BandApi.Services
{
    public interface IBandAlbumRepository
    {
        IEnumerable<Album> GetAlbums(Guid bandId);
        Album GetAlbum(Guid bandId, Guid albumId);
        void AddAlbum(Guid bandId, Album album);
        void UpdateAlbum(Album album);
        void DeleteAlbum(Album album);

        IEnumerable<BandDto> GetBands();
        Band GetBand(Guid bandId);
        IEnumerable<Band> GetBands(IEnumerable<Guid> bandIds);
        void AddBand(Band band);
        void UpdateBand(Band band);
        void DeleteBand(Band band);
        bool BandExists(Guid bandId);
        bool AlbumExists(Guid albumId);
        bool Save();
    }
}
