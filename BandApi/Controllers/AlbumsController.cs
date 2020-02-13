using System;
using System.Collections.Generic;
using AutoMapper;
using BandApi.Entities;
using BandApi.Models;
using BandApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BandApi.Controllers
{
    [ApiController]
    [Route("api/bands/{bandId}/albums")]
    public class AlbumsController : ControllerBase
    {
        private readonly IBandAlbumRepository _bandAlbumRepository;
        private readonly IMapper _mapper;

        public AlbumsController(IBandAlbumRepository bandAlbumRepository, IMapper mapper)
        {
            _bandAlbumRepository = bandAlbumRepository ?? throw new ArgumentNullException(nameof(bandAlbumRepository));
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AlbumDto>> GetAlbumsForBand(Guid bandId)
        {
            if (!_bandAlbumRepository.BandExists(bandId))
                return NotFound();

            var albumsFromRepo = _bandAlbumRepository
                .GetAlbums(bandId);
            return Ok(_mapper.Map<IEnumerable<AlbumDto>>(albumsFromRepo));
        }

        [HttpGet("{albumId}", Name = nameof(GetAlbumForBand))]
        public ActionResult<AlbumDto> GetAlbumForBand(Guid bandId, Guid albumId)
        {
            if (!_bandAlbumRepository.BandExists(bandId))
                return NotFound();

            var album = _bandAlbumRepository.GetAlbum(bandId, albumId);
            if (album == null)
                return NotFound();

            return Ok(_mapper.Map<AlbumDto>(album));
        }

        [HttpPost]
        public ActionResult<AlbumDto> CreateAlbumForBand([FromRoute] Guid bandId, [FromBody] AlbumForCreateDto album)
        {
            if (!_bandAlbumRepository.BandExists(bandId))
                return NotFound();

            var albumEntity = _mapper.Map<Album>(album);
            _bandAlbumRepository.AddAlbum(bandId, albumEntity);
            _bandAlbumRepository.Save();

            var albumToReturn = _mapper.Map<AlbumDto>(albumEntity);
            return CreatedAtRoute(nameof(GetAlbumForBand), new { bandId = bandId, albumId = albumToReturn.Id}, albumToReturn);
        }
    }
}
