using NzWalks.Api.Models;
using NzWalks.Api.Models.Domain;
using NzWalks.Api.Models.DTO;
using NzWalks.Api.Models.DTO.RegionsDtos;
using NzWalks.Api.Models.DTO.WalksDtos;

namespace NzWalks.Api.Repositories
{
    public interface IWalkRepository
    {
        
        Task<ServiceResponse<List<WalkDto>>> GetAllAsyncr(PaginationParams? @params,
            string? FilterBy,
            string? FilterQuery, string? SortBy,
            bool IsAccending = true
           );
        Task<ServiceResponse<List<Walk>>> GetALlAsync(string? FilterBy, 
            string? FilterQuery, string? SortBy, 
            bool IsAccending = true,int PageNumber=1,int PageSize = 1000);
        Task<Walk> GetByIDAsync(Guid id); 
        Task<ServiceResponse<Walk>> AddWalkAsync(AddWalkRequestDto addWalkRequestDto);
        Task<ServiceResponse<WalkDto?>> UpdateWalkAsync(Guid id, AddWalkRequestDto walk);
        Task<ServiceResponse<WalkDto?>> DeleteWalkAsync(Guid id);
        Task<ServiceResponse<Walk?>> GetByIDWithIncludeRegionDefc(Guid id);



    }
}
 