using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NZWalks.API.repository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionController(IRegionRepository regionRepository,IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var region = await regionRepository.GetAllAsync();

            //return DTO region

            // var regionDTO = new List<Models.DTO.Region>();
            // region.ToList().ForEach(x =>
            //{
            //    var RegionDTO = new Models.DTO.Region
            //    {
            //        Id = x.Id,
            //        Name = x.Name,
            //        Code = x.Code,
            //        Area = x.Area,
            //        Lat = x.Lat,
            //        Long = x.Long,
            //        Population = x.Population
            //    };
            //    regionDTO.Add(RegionDTO);
            //});
            var regionDTO =  mapper.Map<List<Models.DTO.Region>>(region);

            return Ok(regionDTO);
        }

    }
}
