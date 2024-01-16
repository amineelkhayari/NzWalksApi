using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NzWalks.Api.Data;
using NzWalks.Api.Models;
using NzWalks.Api.Models.Domain;
using NzWalks.Api.Models.DTO.RegionsDtos;
using NzWalks.Api.Models.DTO.WalksDtos;
using System.Text.Json;

namespace NzWalks.Api.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NzWalksDbContext dbContext;
        private readonly IMapper mapper;

        public SQLWalkRepository(NzWalksDbContext dbContext,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<ServiceResponse<Walk>> AddWalkAsync(AddWalkRequestDto walk)
        {
            var serviceResponse = new ServiceResponse<Walk>();
            try
            {
                var walkDomainModel = mapper.Map<Walk>(walk);
                await dbContext.Walks.AddAsync(walkDomainModel);
                await dbContext.SaveChangesAsync();
                serviceResponse.Data = walkDomainModel;
            }
            catch(Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }
            
            return serviceResponse;
        }

        public async Task<ServiceResponse<WalkDto?>> DeleteWalkAsync(Guid id)
        {
            ServiceResponse<WalkDto> serviceResponse = new ServiceResponse<WalkDto>();

            try
            {
                Walk deletedWalk = await GetByIDAsync(id);
                if (deletedWalk == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Success = false;
                    serviceResponse.Message = "User With Id " + id + " doesn't Exist";
                }
                else
                {
                    dbContext.Walks.Remove(deletedWalk);
                    dbContext.SaveChangesAsync();
                    serviceResponse.Data = mapper.Map<WalkDto>(deletedWalk);
                    serviceResponse.Message = "Walker is deleted";
                }
            }
            catch(Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }
        public async Task<ServiceResponse<List<Walk>>> GetALlAsync(string? FilterBy = null, string? FilterQuery = null,
            string? SortBy = null, bool IsAccending=true,int PageNumber = 1, int PageSize = 1000)
        {
            ServiceResponse<List<Walk>> serviceResponse = new ServiceResponse<List<Walk>>();
            try
            {
                var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();
                //as Queryable make the data able to filtering in and it not a list any more

                //ISBullOrWithSpace check if the inpute has any space or if it null
                if (string.IsNullOrWhiteSpace(FilterBy) == false && string.IsNullOrWhiteSpace(FilterQuery) == false)
                {
                    if (FilterBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                    {
                        walks = walks.Where(x => x.Name.Contains(FilterQuery));
                    }
                }
                //applied Sorting 
                //when i applied sorting i shoud check th e= filter name first 
                if (string.IsNullOrWhiteSpace(SortBy) == false)
                {
                    if (SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                    {
                        walks = IsAccending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                    }
                    else if (SortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                    {
                        walks = IsAccending ? walks.OrderBy(x => x.LenghtInKm) : walks.OrderByDescending(x => x.LenghtInKm);
                    }


                }
                //applied pagination 
                var SkipResulte = (PageNumber - 1) * PageSize;

                serviceResponse.Data = await walks.Skip(SkipResulte).Take(PageSize).ToListAsync();



            }
            catch  (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message= ex.Message;
            }
           

           
            return serviceResponse; 
           // return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }
        private async Task<Walk?> GetWalksById(Guid id) =>
           await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
        public async Task<Walk?> GetByIDAsync(Guid id)
        {
            return  await dbContext.Walks.FirstOrDefaultAsync(w => w.Id == id);
            
        }
        public async Task<ServiceResponse<Walk?>> GetByIDWithIncludeRegionDefc(Guid id)
        {
            ServiceResponse<Walk> serviceResponse = new ServiceResponse<Walk>();
            try
            {

                var walkDomainModel = await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(walk => walk.Id == id);
                if (walkDomainModel == null)
                {   
                    serviceResponse.Success = false;
                    serviceResponse.Message = "This walker doesnt exist";
                }
                else
                {
                    serviceResponse.Data = walkDomainModel;

                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message= ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<WalkDto?>> UpdateWalkAsync(Guid id, AddWalkRequestDto walk)
        {
            ServiceResponse<WalkDto?> serviceResponse = new ServiceResponse<WalkDto?>();
            try
            {
                var existingWalk = await GetWalksById(id);
                if (existingWalk == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "walker with this id doesnt exist " + id;
                }
                existingWalk = mapper.Map(walk,existingWalk);
                dbContext.Update(existingWalk);

                await dbContext.SaveChangesAsync();
                serviceResponse.Data = mapper.Map<WalkDto>(existingWalk);
                serviceResponse.Message = "Walker with name" + existingWalk.Name +"Is Updated";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message= ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<WalkDto>>> GetAllAsyncr(PaginationParams? @params, 
            string? FilterBy, string? FilterQuery, string? SortBy, bool IsAccending = true)
        {

            ServiceResponse<List<WalkDto>> serviceResponse = new ServiceResponse<List<WalkDto>>();
            try
            {
                var regionfilter = dbContext.Walks.AsQueryable();
                if (string.IsNullOrWhiteSpace(FilterBy) == false & string.IsNullOrWhiteSpace(FilterQuery) == false)
                {
                    if (FilterBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                    {
                        regionfilter = regionfilter.Where(regx => regx.Name.Contains(FilterQuery));
                    }
                }
                var regionfilters =  regionfilter.Select(c => mapper.Map<WalkDto>(c));
                var pageMeta = new PaginationMetaData(regionfilters.Count(), @params.page, @params.itemPerPage);

                serviceResponse.Data = await regionfilters.Skip((@params.page - 1) * @params.itemPerPage)
                    .Take(@params.itemPerPage).ToListAsync();
                serviceResponse.Message = JsonSerializer.Serialize(pageMeta);


                //serviceResponse.Message = "data loaded";

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

            }
            return serviceResponse;


        }
    }
}
