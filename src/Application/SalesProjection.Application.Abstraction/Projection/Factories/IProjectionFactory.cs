using SalesProjection.Application.Abstraction.Projection.Strategies;
using SalesProjection.Domain.DTOs;

namespace SalesProjection.Application.Projection.Factories
{
    public interface IProjectionFactory
    {
        IProjectionStrategy CreateStrategy(LotProjectionDTO lotProjection);
    }
}
