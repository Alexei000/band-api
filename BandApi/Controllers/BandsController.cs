using System;
using System.Collections.Generic;
using AutoMapper;
using BandApi.Entities;
using BandApi.Helpers;
using BandApi.Models;
using BandApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BandApi.Controllers
{
    [ApiController]
    [Route("api/bands")]
    public class BandsController : ControllerBase
    {
        private readonly IBandAlbumRepository _bandAlbumRepository;
        private readonly IMapper _mapper;

        public BandsController(IBandAlbumRepository bandAlbumRepository, IMapper mapper)
        {
            _bandAlbumRepository = bandAlbumRepository ?? throw new ArgumentNullException(nameof(bandAlbumRepository));
            _mapper = mapper;
        }

        [HttpHead]
        [HttpGet]
        public ActionResult<IEnumerable<BandDto>> GetBands([FromQuery] BandsResourceParameters bandsResParams)
        {
            var repoBands = _bandAlbumRepository.GetBands(bandsResParams);
            return Ok(repoBands);
        }

        [HttpGet("{bandId}", Name = nameof(GetBand))]
        public IActionResult GetBand(Guid bandId)
        {
            var band = _bandAlbumRepository.GetBand(bandId);
            return band == null ? (IActionResult) NotFound() : Ok(band);
        }

        [HttpPost]
        public ActionResult<BandDto> CreateBand([FromBody] BandForCreateDto band)
        {
            var bandEntity = _mapper.Map<Band>(band);
            _bandAlbumRepository.AddBand(bandEntity);
            _bandAlbumRepository.Save();

            var bandToReturn = _mapper.Map<BandDto>(bandEntity);
            return CreatedAtRoute(nameof(GetBand), new { bandId = bandToReturn.Id}, bandToReturn);
        }

        [HttpOptions]
        public IActionResult GetBandsOptions()
        {
            Response.Headers.Add("Allow", "GET,POST,DELETE,HEAD,OPTIONS");
            return Ok();
        }
    }
}
