using Microsoft.Extensions.DependencyInjection;

using SalesProjection.Application.Abstraction.Projection.Strategies;
using SalesProjection.Application.Projection.Strategies;
using SalesProjection.Domain.DTOs;

namespace SalesProjection.Application.Projection.Factories
{
    public class ProjectionFactory: IProjectionFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ProjectionFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IProjectionStrategy CreateStrategy(LotProjectionDTO lotProjection)
        {
            if (lotProjection.Region == "sudeste")
                return _serviceProvider.GetRequiredService<DailyAveragesSalesFromLastSevenDaysStrategy>();
            else
                return _serviceProvider.GetRequiredService<DailyAveragesPreviouMonthStrategy>();
        }
        
    }
}
