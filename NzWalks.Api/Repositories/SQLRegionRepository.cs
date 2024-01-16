using AutoMapper;
using Azure;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using NzWalks.Api.Data;
using NzWalks.Api.Models;
using NzWalks.Api.Models.Domain;
using NzWalks.Api.Models.DTO.RegionsDtos;
using NzWalks.Api.Models.DTO.WalksDtos;
using System.Net;
using System.Text.Json;

namespace NzWalks.Api.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NzWalksDbContext dbContext;
        private readonly IMapper mapper;

        public SQLRegionRepository(NzWalksDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        // save data to data base
        public async Task<ServiceResponse<Region>> CreateAsync(AddRegionRequestDto addRegionRequestDto)
        {
            var serviceResponse = new ServiceResponse<Region>();
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);
            try
            {
                await dbContext.Regions.AddAsync(regionDomainModel);
                await dbContext.SaveChangesAsync();
                serviceResponse.Data = regionDomainModel;
                serviceResponse.Message = "Region With Code: '" + regionDomainModel.Code + "' Add With Success";

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;

        }



        public async Task<ServiceResponse<RegionDto?>> DeleteAsync(Guid id)
        {
            ServiceResponse<RegionDto> serviceResponse = new ServiceResponse<RegionDto>();

            var DomainRegionModel = await GetUserById(id);
            try
            {
                serviceResponse.Data = mapper.Map<RegionDto>(DomainRegionModel);
                if (DomainRegionModel == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Success = false;
                    serviceResponse.Message = "User With Id " + id + " doesn't Exist";
                }
                else
                {
                    dbContext.Regions.Remove(DomainRegionModel);
                    await dbContext.SaveChangesAsync();
                    serviceResponse.Data = mapper.Map<RegionDto>(DomainRegionModel);
                    serviceResponse.Message = "The Region With Code : " + DomainRegionModel.Code;

                }

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

            }
            return serviceResponse;


        }
        public async Task<ServiceResponse<List<RegionOptoionDto>>> GetAllIDAndCodeSelect()
        {
            ServiceResponse<List<RegionOptoionDto>> serviceResponse = new ServiceResponse<List<RegionOptoionDto>>();

            try
            {
              var regions =await  dbContext.Regions.ToListAsync();

                serviceResponse.Data = regions.Select(region => mapper.Map<RegionOptoionDto>(region)).ToList();
                serviceResponse.Message = "Data Get In With Success";
            }catch(Exception ex) { 
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<RegionDto>>> GetAllAsyncr(string? FilterBy,
           string? FilterQuery,
           PaginationParams? @params)
        {
            ServiceResponse<List<RegionDto>> serviceResponse = new ServiceResponse<List<RegionDto>>();
            try
            {
                var regionfilter = dbContext.Regions.AsQueryable();
                if (string.IsNullOrWhiteSpace(FilterBy) == false & string.IsNullOrWhiteSpace(FilterQuery) == false)
                {
                    if (FilterBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                    {
                        regionfilter = regionfilter.Where(regx => regx.Name.Contains(FilterQuery));
                    }
                }
                var regionfilters = regionfilter.Select(c => mapper.Map<RegionDto>(c));
                var pageMeta = new PaginationMetaData(regionfilters.Count(), @params.page, @params.itemPerPage);

                serviceResponse.Data = regionfilters.Skip((@params.page - 1) * @params.itemPerPage)
                    .Take(@params.itemPerPage).ToList();
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
        public async Task<ServiceResponse<List<RegionDto>>> GetAllAsync(string? FilterBy = null, string? FilterQuery = null)
        {
            ServiceResponse<List<RegionDto>> serviceResponse = new ServiceResponse<List<RegionDto>>();
            try
            {
                var regionfilter = dbContext.Regions.AsQueryable();
                if (string.IsNullOrWhiteSpace(FilterBy) == false & string.IsNullOrWhiteSpace(FilterQuery) == false)
                {
                    if (FilterBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                    {
                        regionfilter = regionfilter.Where(regx => regx.Name.Contains(FilterQuery));
                    }
                }
                serviceResponse.Data = regionfilter.Select(c => mapper.Map<RegionDto>(c)).ToList();
                serviceResponse.Message = "data loaded";

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
           
            return serviceResponse;
        }
        private async Task<Region?> GetUserById(Guid id)=>
            await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        
            
        public async Task<ServiceResponse<RegionDto?>> GetByIdAsync(Guid id)
        {
            ServiceResponse<RegionDto> serviceResponse = new ServiceResponse<RegionDto>();
            try
            {
                var data = await GetUserById(id);
                if(data == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "This Region With Id "+id+" does exist";
                }
                else
                {
                    serviceResponse.Data = mapper.Map<RegionDto>(data);
                    serviceResponse.Message = "Region Found "+data.Code;
                }
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message =ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<RegionDto?>> UpdateAsync(Guid id, UpdateRegionRequestDto region
            )
        {
            ServiceResponse<RegionDto?> serviceResponse = new ServiceResponse<RegionDto?>();
            try
            {
                var data = await GetUserById(id);
                if (data != null)
                { 
                    data = mapper.Map(region, data);
                    serviceResponse.Data = mapper.Map<RegionDto>(data);
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "This User With Id " + id + " Doesn't exist";
                }
                await dbContext.SaveChangesAsync();

            }
            catch(Exception ex)
            {
                serviceResponse.Success=false;
                serviceResponse.Message=ex.Message;
            }
            return serviceResponse;

            // end func update 
        }

 
    }
}
