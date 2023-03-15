using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.repository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionController(IRegionRepository regionRepository, IMapper mapper)
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
            var regionDTO = mapper.Map<List<Models.DTO.Region>>(region);

            return Ok(regionDTO);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        [ActionName("GetRegionbyId")]
        public async Task<IActionResult> GetRegionbyId(Guid id)
        {
            var region = await regionRepository.GetByIdAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            var regionDTO = mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);
        }

        [HttpPost]

        public async Task<IActionResult> AddRegionAsync(addRegionRequest addregionrequest)
        {
            //Request to Domain Model
            var region = new Models.Domain.Region
            {
                Code = addregionrequest.Code,
                Name = addregionrequest.Name,
                Area = addregionrequest.Area,
                Lat = addregionrequest.Lat,
                Long = addregionrequest.Long,
               Population = addregionrequest.Population
            };

            //pass region to repository
            region = await regionRepository.AddAsync(region);

            //Convert back to DTO
            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population
            };
            return CreatedAtAction(nameof(GetRegionbyId), new { region.Id }, regionDTO);
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            //get region from database
            var region = await regionRepository.DeleteAsync(id);

            //if Null notfound
            if (region == null)
                return NotFound();

            //convert response back to DTO
            var regionDTO = mapper.Map<Models.DTO.Region>(region);

            return Ok(regionDTO);

        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateRegionAsync(Guid id, [FromBody] Models.DTO.updateRegionRequest updateRegionRequest)
        {

            //convert DTO to domain
            var region = new Models.Domain.Region { 
                Id = id,
                Name = updateRegionRequest.Name,
                Code = updateRegionRequest.Code,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Population = updateRegionRequest.Population  
            };

            //update region using regionrepository
            var existingRegion = await regionRepository.UpdateAsync(id, region);

            //check if region null
            if(existingRegion == null)
            {
                return NotFound();
            }
            //convet domain to DTO

            //Convert back to DTO
            var regionDTO = new Models.DTO.updateRegionRequest
            {
                Code = region.Code,
                Name = region.Name,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population
            };
            return Ok(regionDTO);
        }
    }
}
