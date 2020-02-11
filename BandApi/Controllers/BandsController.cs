using System;
using System.Collections.Generic;
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

        public BandsController(IBandAlbumRepository bandAlbumRepository)
        {
            _bandAlbumRepository = bandAlbumRepository ?? throw new ArgumentNullException(nameof(bandAlbumRepository));
        }

        [HttpGet]
        public ActionResult<IEnumerable<BandDto>> GetBands()
        {
            var repoBands = _bandAlbumRepository.GetBands();
            return Ok(repoBands);
        }

        [HttpGet("{bandId}")]
        public IActionResult GetBand(Guid bandId)
        {
            var band = _bandAlbumRepository.GetBand(bandId);
            return band == null ? (IActionResult) NotFound() : Ok(band);
        }
    }
}
