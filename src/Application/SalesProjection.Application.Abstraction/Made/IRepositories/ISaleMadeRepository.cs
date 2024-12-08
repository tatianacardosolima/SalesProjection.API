using SalesProjection.Domain.DTOs;
using SalesProjection.Domain.Entities;

namespace SalesProjection.Application.Abstraction.Made.IRepositories
{
    public interface IProcessLotRepository : IDefaultRepository<ProcessLot>
    {
        Task<List<LotProjectionDTO>> GetBranchToProjectionByYearAndMonthAsync(int year, int month);
        Task<List<SaleMadeItem>> GetLastMonthByBranchAndPeriodAsync(string branch, string period);
    }
}
