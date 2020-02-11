using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AutoMapper;
using BandApi.DbContexts;
using BandApi.Entities;
using BandApi.Helpers;
using BandApi.Models;

namespace BandApi.Services
{
    public class BandAlbumRepository : IBandAlbumRepository
    {
        private readonly BandAlbumContext _context;
        private readonly IMapper _mapper;

        public BandAlbumRepository(BandAlbumContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper;
        }

        public IEnumerable<Album> GetAlbums(Guid bandId)
        {
            if (bandId == Guid.Empty)
                throw new ArgumentNullException(nameof(bandId));

            return _context.Albums
                .Where(a => a.BandId == bandId)
                .OrderBy(a => a.Title)
                .ToList();
        }

        public Album GetAlbum(Guid bandId, Guid albumId)
        {
            if (bandId == Guid.Empty)
                throw new ArgumentNullException(nameof(bandId));
            if (albumId == Guid.Empty)
                throw new ArgumentNullException(nameof(albumId));

            return _context.Albums.FirstOrDefault(a => a.Id == albumId && a.BandId == bandId);
        }

        public void AddAlbum(Guid bandId, Album album)
        {
            if (bandId == Guid.Empty)
                throw new ArgumentNullException(nameof(bandId));
            if (album == null)
                throw new ArgumentNullException(nameof(album));

            album.BandId = bandId;
            _context.Albums.Add(album);
        }

        public void UpdateAlbum(Album album)
        {
            // not implemented
        }

        public void DeleteAlbum(Album album)
        {
            if (album == null)
                throw new ArgumentNullException(nameof(album));

            _context.Albums.Remove(album);
        }

        public IEnumerable<BandDto> GetBands()
        {
            var bands = _context.Bands.ToList();
            var bandsDto = _mapper.Map<IEnumerable<BandDto>>(bands);
            return bandsDto;
        }

        public Band GetBand(Guid bandId)
        {
            if (bandId == Guid.Empty)
                throw new ArgumentNullException(nameof(bandId));

            return _context.Bands.FirstOrDefault(b => b.Id == bandId);
        }

        public IEnumerable<Band> GetBands(IEnumerable<Guid> bandIds)
        {
            if (bandIds == null)
                throw new ArgumentNullException(nameof(bandIds));

            return _context.Bands
                .Where(b => bandIds.Contains(b.Id))
                .OrderBy(b => b.Name);
        }

        public void AddBand(Band band)
        {
            if (band == null)
                throw new ArgumentNullException(nameof(band));

            _context.Bands.Add(band);
        }

        public void UpdateBand(Band band)
        {
            // not implemented
        }

        public void DeleteBand(Band band)
        {
            if (band == null)
                throw new ArgumentNullException(nameof(band));

            _context.Bands.Remove(band);
        }

        public bool BandExists(Guid bandId)
        {
            if (bandId == Guid.Empty)
                throw new ArgumentNullException(nameof(bandId));

            return _context.Bands.Any(a => a.Id == bandId);
        }

        public bool AlbumExists(Guid albumId)
        {
            if (albumId == Guid.Empty)
                throw  new ArgumentNullException(nameof(albumId));

            return _context.Albums.Any(a => a.Id == albumId);
        }

        public bool Save()
        {
            return _context.SaveChanges() < 0;
        }
    }
}
