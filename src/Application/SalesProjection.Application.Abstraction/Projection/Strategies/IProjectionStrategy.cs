using SalesProjection.Application.Abstraction.Projection.Response;
using SalesProjection.Domain.DTOs;

namespace SalesProjection.Application.Abstraction.Projection.Strategies
{
    public interface IProjectionStrategy
    {
        Task<ProjectionResponse> GetProjectionAsync(LotProjectionDTO lotProjection, DateTime current);
    }
}
