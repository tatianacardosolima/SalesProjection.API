using SalesProjection.Application.Abstraction.Made.IRepositories;
using SalesProjection.Application.Abstraction.Projection.Response;
using SalesProjection.Application.Abstraction.Projection.Strategies;
using SalesProjection.Domain.DTOs;
using SalesProjection.Domain.Entities;

namespace SalesProjection.Application.Projection.Strategies
{
    public class DailyAveragesSalesFromLastSevenDaysStrategy : IProjectionStrategy
    {
        private readonly IProcessLotRepository _repository;

        public DailyAveragesSalesFromLastSevenDaysStrategy(IProcessLotRepository repository)
        {
            _repository = repository;
        }
        public async Task<ProjectionResponse> GetProjectionAsync(LotProjectionDTO lotProjection, DateTime current)
        {
            List<SaleMadeItem> list = await _repository.GetLastMonthByBranchAndPeriodAsync(lotProjection.Branch, lotProjection.Period);
            var response = new ProjectionResponse(lotProjection.Branch, lotProjection.Period, lotProjection.Period, 0, "DailyAveragesSalesFromLastSevenDaysStrategy");
            var lastDayMonth = new DateTime(int.Parse(lotProjection.Period.Split("-")[0]),int.Parse(lotProjection.Period.Split("-")[1]) ,1);
            lastDayMonth = lastDayMonth.AddMonths(1).AddDays(-1);

            list = list.Where(x => x.Period >= lastDayMonth.AddDays(-7)).ToList();
            var projectionList = new List<SaleMadeItem>();

            var firstDay = new DateTime(current.Year, current.Month, 01);
            var lastDay = firstDay.AddMonths(1).AddDays(-1);
            for (var day = firstDay; day <= lastDay; day = day.AddDays(1))
            {
                for (int hour = 1; hour <= 24; hour++)
                {
                    var dayOfWeek = day.DayOfWeek;
                    var salemade = list.Where(x => x.Period.DayOfWeek == dayOfWeek && x.Hour == hour).LastOrDefault();
                    var saleMadeItem = new SaleMadeItem(day,hour, salemade?.Sale??0);
                    projectionList.Add(saleMadeItem);
                }
            }
            var dailyAverages = projectionList
            .GroupBy(sale => sale.Period)
            .Select(group => new
            {
                Period = group.Key,
                DailyAverage = group.Average(sale => sale.Sale)
            })
            .ToList();
            
            return new ProjectionResponse(lotProjection.Branch, lotProjection.Region, current.ToString("yyyy-MM"), dailyAverages.Sum(x => x.DailyAverage), "DailyAveragesSalesFromLastSevenDaysStrategy"); ;
        }
    }
}
