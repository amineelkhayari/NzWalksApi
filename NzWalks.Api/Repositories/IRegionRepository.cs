using Microsoft.AspNetCore.Mvc;
using NzWalks.Api.Models;
using NzWalks.Api.Models.Domain;
using NzWalks.Api.Models.DTO.RegionsDtos;
using NzWalks.Api.Models.DTO.WalksDtos;

namespace NzWalks.Api.Repositories
{
    public interface IRegionRepository
    {
        Task<ServiceResponse<List<RegionOptoionDto>>> GetAllIDAndCodeSelect(
           );
        Task<ServiceResponse<List<RegionDto>>> GetAllAsync(string? FilterBy, 
           string? FilterQuery);
        Task<ServiceResponse<List<RegionDto>>> GetAllAsyncr(string? FilterBy,
           string? FilterQuery,
           PaginationParams? @params);
        Task<ServiceResponse<RegionDto?>> GetByIdAsync(Guid id);
        Task<ServiceResponse<Region>> CreateAsync(AddRegionRequestDto region);
        Task<ServiceResponse<RegionDto?>> UpdateAsync(Guid id, UpdateRegionRequestDto region);
        Task<ServiceResponse<RegionDto?>> DeleteAsync(Guid id);


    }
}
