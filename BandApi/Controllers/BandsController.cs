using System;
using System.Collections.Generic;
using AutoMapper;
using BandApi.Entities;
using BandApi.Helpers;
using BandApi.Models;
using BandApi.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BandApi.Controllers
{
    [ApiController]
    [Route("api/bands")]
    public class BandsController : ControllerBase
    {
        private readonly IBandAlbumRepository _bandAlbumRepository;
        private readonly IMapper _mapper;
        private IPropertyMappingService _propertyMappingService;

        public BandsController(IBandAlbumRepository bandAlbumRepository, IMapper mapper,
            IPropertyMappingService propertyMappingService)
        {
            _bandAlbumRepository = bandAlbumRepository ?? throw new ArgumentNullException(nameof(bandAlbumRepository));
            _mapper = mapper;
            _propertyMappingService = propertyMappingService ?? throw new ArgumentNullException(nameof(propertyMappingService));
        }

        private string CreateBandsUri(BandsResourceParameters bandsResourceParameters, UriType uriType)
        {
            return uriType switch
            {
                UriType.PreviousPage => Url.Link(nameof(GetBands), new
                {
                    orderBy = bandsResourceParameters.OrderBy,
                    pageNumber = bandsResourceParameters.PageNumber - 1,
                    pageSize = bandsResourceParameters.PageSize,
                    mainGenre = bandsResourceParameters.MainGenre,
                    searchQuery = bandsResourceParameters.SearchQuery
                }),
                UriType.NextPage => Url.Link(nameof(GetBands), new
                {
                    orderBy = bandsResourceParameters.OrderBy,
                    pageNumber = bandsResourceParameters.PageNumber + 1,
                    pageSize = bandsResourceParameters.PageSize,
                    mainGenre = bandsResourceParameters.MainGenre,
                    searchQuery = bandsResourceParameters.SearchQuery
                }),
                _ => Url.Link(nameof(GetBands), new
                {
                    orderBy = bandsResourceParameters.OrderBy,
                    pageNumber = bandsResourceParameters.PageNumber,
                    pageSize = bandsResourceParameters.PageSize,
                    mainGenre = bandsResourceParameters.MainGenre,
                    searchQuery = bandsResourceParameters.SearchQuery
                }),
            };
        }

        [HttpHead]
        [HttpGet(Name = nameof(GetBands))]
        public ActionResult<IEnumerable<BandDto>> GetBands([FromQuery] BandsResourceParameters bandsResParams)
        {
            if (!_propertyMappingService.ValidMappingExists<BandDto, Band>(bandsResParams.OrderBy))
                return BadRequest("Invalid mapping");

            var repoBands = _bandAlbumRepository.GetBands(bandsResParams);

            string prevPageLink = repoBands.HasPrevious ? CreateBandsUri(bandsResParams, UriType.PreviousPage) : null;
            string nextPageLink = repoBands.HasNext ? CreateBandsUri(bandsResParams, UriType.NextPage) : null;

            var metadata = new
            {
                totalCount = repoBands.TotalCount,
                pageSize = repoBands.PageSize,
                currentPage = repoBands.CurrentPage,
                totalPages = repoBands.TotalPages,
                prevPageLink,
                nextPageLink
            };

            Response.Headers.Add("Pagination", JsonConvert.SerializeObject(metadata));

            var bandToReturn = _mapper.Map<IEnumerable<BandDto>>(repoBands);
            return Ok(bandToReturn);
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
