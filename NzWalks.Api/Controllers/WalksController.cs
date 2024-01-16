using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NzWalks.Api.Models;
using NzWalks.Api.Models.Domain;
using NzWalks.Api.Models.DTO.WalksDtos;
using NzWalks.Api.Repositories;
using System.ComponentModel.DataAnnotations;

namespace NzWalks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper,IWalkRepository walkRepository) {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }
        //Create walk
        //Post:/api/walks
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Walk>>> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            return Ok(await walkRepository.AddWalkAsync(addWalkRequestDto));
         }

        //get all walks 
        // GET : /api/walks?filterOn=Name&&filterQuery="someword"&isAscieding&pageNumer=1&pageSize=10

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Walk>>>> GetAllWalks([FromQuery]string? FilterBy, [FromQuery] string? FilterQuery, [FromQuery] string? SortBy,
            [FromQuery] bool? IsAccending, [FromQuery] PaginationParams @params)
        {
            var walksDomain = await walkRepository.GetAllAsyncr(@params,FilterBy, FilterQuery, SortBy,
               IsAccending ?? true);

            //var walksDomain = await walkRepository.GetALlAsync(FilterBy, FilterQuery, SortBy,IsAccending??true,pageNumber,pageSize);
            return Ok(walksDomain); 
        }
        [HttpGet]
        [Route("GetWalkById/{walkId:Guid}")]
        public async Task<ActionResult<ServiceResponse<Walk?>>> GetByIDWithIncludeRegDefc(Guid walkId)
        {
            var walksDomain = await walkRepository.GetByIDWithIncludeRegionDefc(walkId);
            return Ok(walksDomain);
        }

        [HttpPut]
        [Route("GetWalkById/{walkId:Guid}")]
        public async Task<IActionResult> UpadteWalk([FromBody] AddWalkRequestDto addWalkRequestDto, [FromRoute] Guid walkId)
        {
            //map DTO to domain model 
            var walkdModelDto = await walkRepository.UpdateWalkAsync(walkId,addWalkRequestDto);
            //map domain model to dto 
            return Ok(walkdModelDto);
        }

        [HttpDelete]
        [Route("DeleteWalkByID/{walkId:Guid}")]
        public async Task<ActionResult<ServiceResponse<WalkDto?>>> DeleteWalk([FromRoute] Guid walkId)
        {
           var WalkDomain= await walkRepository.DeleteWalkAsync(walkId);
           return Ok(WalkDomain);
        }
    }
}
