using SalesProjection.Application.Abstraction.Made.IRepositories;
using SalesProjection.Application.Abstraction.Projection.Response;
using SalesProjection.Application.Projection.Factories;

namespace SalesProjection.Application.Projection.UseCases
{
    public class GetProjectCurrentMonthUseCase : IGetProjectCurrentMonthUseCase
    {
        private readonly IProcessLotRepository _repository;
        private readonly IProjectionFactory _factory;

        public GetProjectCurrentMonthUseCase(IProcessLotRepository repository,
                    IProjectionFactory factory
                )
        {
            _repository = repository;
            _factory = factory;
        }
        public async Task<List<ProjectionResponse>> GetProjectionAsync()
        {
            var lastMonthDate = DateTime.UtcNow.AddMonths(-1);
            var branchs = await _repository.GetBranchToProjectionByYearAndMonthAsync(lastMonthDate.Year, lastMonthDate.Month);
            var list = new List<ProjectionResponse>();
            foreach (var branch in branchs)
            {
                var strategy  = _factory.CreateStrategy(branch);
                list.Add( await strategy.GetProjectionAsync(branch, DateTime.UtcNow));
            }
            return list;
        }
    }
}
