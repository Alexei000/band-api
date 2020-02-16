using System;
using System.Collections.Generic;
using AutoMapper;
using BandApi.Entities;
using BandApi.Models;
using BandApi.Services;
using Microsoft.AspNetCore.JsonPatch;
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
            return CreatedAtRoute(nameof(GetAlbumForBand), new {bandId, albumId = albumToReturn.Id}, albumToReturn);
        }

        [HttpPut("{albumId}")]
        public ActionResult UpdateAlbumForBand(Guid bandId, Guid albumId, [FromBody] AlbumForUpdateDto album)
        {
            if (!_bandAlbumRepository.BandExists(bandId))
                return NotFound();
            var albumEntity = _bandAlbumRepository.GetAlbum(bandId, albumId);
            if (albumEntity == null)
            {
                // return NotFound();
                var albumToAdd = _mapper.Map<Album>(album);
                albumToAdd.Id = albumId;
                _bandAlbumRepository.AddAlbum(bandId, albumToAdd);
                _bandAlbumRepository.Save();

                var albumToReturn = _mapper.Map<AlbumDto>(albumToAdd);

                return CreatedAtRoute(nameof(GetAlbumForBand), new {bandId, albumId = albumToReturn.Id},
                    albumToReturn);
            }

            _mapper.Map(album, albumEntity);
            _bandAlbumRepository.UpdateAlbum(albumEntity);
            _bandAlbumRepository.Save();
            return NoContent();
        }

        [HttpPatch("{albumId}")]
        public ActionResult PartiallyUpdateAlbumForBand(Guid bandId, Guid albumId, 
            [FromBody] JsonPatchDocument<AlbumForUpdateDto> patchDocument)
        {
            if (!_bandAlbumRepository.BandExists(bandId))
                return NotFound();
            var albumEntity = _bandAlbumRepository.GetAlbum(bandId, albumId);
            if (albumEntity == null)
            {
                // return NotFound();
                var albumDto = new AlbumForUpdateDto();
                patchDocument.ApplyTo(albumDto);
                var albumToAdd = _mapper.Map<Album>(albumDto);
                albumToAdd.Id = albumId;
                _bandAlbumRepository.AddAlbum(bandId, albumToAdd);
                _bandAlbumRepository.Save();

                var albumToReturn = _mapper.Map<AlbumDto>(albumToAdd);
                return CreatedAtRoute(nameof(GetAlbumForBand), new { bandId, albumId = albumToReturn.Id },
                    albumToReturn);
            }

            var albumToPatch = _mapper.Map<AlbumForUpdateDto>(albumEntity);
            patchDocument.ApplyTo(albumToPatch, ModelState);

            if (!TryValidateModel(albumToPatch))
                return ValidationProblem(ModelState);

            _mapper.Map(albumToPatch, albumEntity);
            _bandAlbumRepository.UpdateAlbum(albumEntity);
            _bandAlbumRepository.Save();
            return NoContent();
        }

        [HttpDelete("{albumId}")]
        public ActionResult DeleteAlbumForBand(Guid bandId, Guid albumId)
        {
            if (!_bandAlbumRepository.BandExists(bandId))
                return NotFound();
            var albumEntity = _bandAlbumRepository.GetAlbum(bandId, albumId);
            if (albumEntity == null)
                return NotFound();

            _bandAlbumRepository.DeleteAlbum(albumEntity);
            _bandAlbumRepository.Save();
            return NoContent();
        }
    }
}
