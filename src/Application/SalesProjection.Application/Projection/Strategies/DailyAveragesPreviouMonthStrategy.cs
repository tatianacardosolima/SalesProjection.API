using SalesProjection.Application.Abstraction.Made.IRepositories;
using SalesProjection.Application.Abstraction.Projection.Response;
using SalesProjection.Application.Abstraction.Projection.Strategies;
using SalesProjection.Domain.DTOs;
using SalesProjection.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesProjection.Application.Projection.Strategies
{
    internal class DailyAveragesPreviouMonthStrategy : IProjectionStrategy
    {
        private IProcessLotRepository _repository;

        public DailyAveragesPreviouMonthStrategy(IProcessLotRepository repository)
        {
            _repository = repository;
        }
        public async Task<ProjectionResponse> GetProjectionAsync(LotProjectionDTO lotProjection, DateTime current)
        {
            List<SaleMadeItem> projectionList = await _repository.GetLastMonthByBranchAndPeriodAsync(lotProjection.Branch, lotProjection.Period);
            var dailyAverages = projectionList
            .GroupBy(sale => sale.Period)
            .Select(group => new
            {
                Period = group.Key,
                DailyAverage = group.Average(sale => sale.Sale)
            })
            .ToList();
            var response = new ProjectionResponse(lotProjection.Branch, DateTime.UtcNow.ToString("yyyy-MM"), lotProjection.Period, dailyAverages.Sum(x => x.DailyAverage), "DailyAveragesPreviouMonthStrategy");
            
            return response;
        }
    }
}
