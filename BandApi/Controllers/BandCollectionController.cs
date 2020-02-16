using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BandApi.Entities;
using BandApi.Helpers;
using BandApi.Models;
using BandApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BandApi.Controllers
{
    [ApiController]
    [Route("api/bandcollections")]
    public class BandCollectionController : ControllerBase
    {
        private readonly IBandAlbumRepository _bandAlbumRepository;
        private readonly IMapper _mapper;

        public BandCollectionController(IBandAlbumRepository bandAlbumRepository, IMapper mapper)
        {
            _bandAlbumRepository = bandAlbumRepository ?? throw new ArgumentNullException(nameof(bandAlbumRepository));
            _mapper = mapper;
        }

        // keys: ...
        [HttpGet("({ids})", Name = nameof(GetBandCollection))]
        
        public IActionResult GetBandCollection([FromRoute] [ModelBinder(typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
                return BadRequest();

            var bandIds = ids.ToList();
            var bandEntities = _bandAlbumRepository.GetBands(bandIds);

            if (bandEntities.Count() != bandIds.Count)
                return NotFound();

            var bandsToReturn = _mapper.Map<IEnumerable<BandDto>>(bandEntities);
            return Ok(bandsToReturn);
        }

        [HttpPost]
        public ActionResult<IEnumerable<BandDto>> CreateBandCollection([FromBody] IEnumerable<BandForCreateDto> bandCollection)
        {
            var bandEntities = _mapper.Map<IEnumerable<Band>>(bandCollection);
            foreach (var band in bandEntities)
            {
                _bandAlbumRepository.AddBand(band);
            }

            _bandAlbumRepository.Save();

            var bandsToReturn = _mapper.Map<IEnumerable<BandDto>>(bandEntities);
            var idsString = string.Join(",", bandsToReturn.Select(b => b.Id));

            return CreatedAtRoute(nameof(GetBandCollection), new {ids = idsString}, bandsToReturn);
        }
    }
}
