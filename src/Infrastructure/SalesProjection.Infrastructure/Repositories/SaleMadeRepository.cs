using Nest;
using SalesProjection.Application.Abstraction.Made.IRepositories;
using SalesProjection.Domain.Entities;
using SalesProjection.Infrastructure.Database.Abstractions;

namespace SalesProjection.Infrastructure.Database.Repositories
{
    public class ProcessLotRepository : DefaultRepository<ProcessLot>, IProcessLotRepository
    {
        public ProcessLotRepository(ElasticClient client)
       : base(client, "sales-projection-processlot") 
        {
        }
    }
}
