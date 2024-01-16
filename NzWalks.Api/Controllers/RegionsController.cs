using Microsoft.AspNetCore.Mvc;
using NzWalks.Api.CustomFiltersActions;
using NzWalks.Api.Models;
using NzWalks.Api.Models.Domain;
using NzWalks.Api.Models.DTO.RegionsDtos;
using NzWalks.Api.Repositories;
using RegionDto = NzWalks.Api.Models.DTO.RegionsDtos.RegionDto;


namespace NzWalks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;

        public RegionsController(IRegionRepository regionRepository)
        {
            this.regionRepository = regionRepository;
        }

        //this methos alow as to get al data about the regions 
        [HttpGet]
        [Route("OptionId")]
        public async Task<ActionResult<ServiceResponse<List<Region>>>> GetAllOption()
        {
            var regionsDomain = await regionRepository.GetAllIDAndCodeSelect();
            return Ok(regionsDomain);
        }
        [HttpGet]
        //[Authorize(Roles = "Reader, Writer")]


        public async Task<ActionResult<ServiceResponse<List<Region>>>> GetAll(
            [FromQuery] string? FilterBy, 
            [FromQuery] string? FilterQuery,
            [FromQuery] PaginationParams? @params
            )
        {
            var regionsDomain = await  regionRepository.GetAllAsyncr(FilterBy != null ? FilterBy.Trim() : null,  FilterQuery != null ? FilterQuery.Trim() : null,@params);
            return Ok(regionsDomain);
        }
        //this methos allow as to  get data by id 
        [HttpGet]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Reader")]
        public async Task<ActionResult<ServiceResponse<RegionDto?>>> GetById([FromRoute]Guid id)
        {
            return Ok(await regionRepository.GetByIdAsync(id));
        }
        [HttpPost]
        //[Authorize(Roles = "Writer")]
        public async Task<ActionResult<ServiceResponse<Region>>> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            if (ModelState.IsValid)
            {
                
                return Ok(await regionRepository.CreateAsync(addRegionRequestDto)); 

            }
            else { return BadRequest(ModelState); }


        }

        //Update Regions 
        //Put : htttp://loca
        //localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("update/{ide:Guid}")]
        //[Authorize(Roles = "Writer")]

        public async Task<ActionResult<ServiceResponse<RegionDto>>> UpdateData([FromRoute] Guid ide, UpdateRegionRequestDto region)

        {
            return Ok(await regionRepository.UpdateAsync(ide, region));
        }



        [HttpDelete]
        [ValidateModelsAttribute]
        [Route("{id}")]
        //[Authorize(Roles = "Writer")]
        public async Task<ActionResult<ServiceResponse<RegionDto?>>> DeleteRegion([FromRoute] Guid id)
        {
            return Ok(await regionRepository.DeleteAsync(id));

        }
    }
}
