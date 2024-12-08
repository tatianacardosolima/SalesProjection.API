using Nest;
using SalesProjection.Application.Abstraction.Made.IRepositories;
using SalesProjection.Domain.DTOs;
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

        public async Task<List<LotProjectionDTO>> GetBranchToProjectionByYearAndMonthAsync(int year, int month)
        {
            var period = $"{year}-{month}";
            var response = await _client.SearchAsync<ProcessLot>(s => s
                            .Index(_indexName) // Substitua pelo nome do índice
                            .Query(q => q
                                .Bool(b => b
                                    .Filter(f => f
                                        .Term(t => t.Field(f => f.Period).Value(period)) // Filtro pelo campo "Period"
                                    )
                                )
                            )
                        );

            if (!response.IsValid || response.Documents == null)
            {
                throw new Exception($"Erro na consulta: {response.DebugInformation}");
            }

            return response.Documents.Select(x => new LotProjectionDTO()
            {
                Branch = x.BranchName,
                Period = x.Period,
                Region = x.Region
            }).ToList();

        }

        public async Task<List<SaleMadeItem>> GetLastMonthByBranchAndPeriodAsync(string branch, string period)
        {
            
            var response =  await _client.SearchAsync<ProcessLot>(s => s
                    .Index("index-name") // Substitua pelo nome do seu índice
                    .Query(q => q
                        .Bool(b => b
                            .Filter(
                                f => f.Term(t => t.Field(p => p.Period).Value(period)),  
                                f => f.Term(t => t.Field(p => p.BranchName).Value(branch))
                            )
                        )
                    )
                );

            if (!response.IsValid || response.Documents == null)
            {
                throw new Exception($"Erro na consulta: {response.DebugInformation}");
            }

            return response.Documents.FirstOrDefault()!.SaleMadeItem;
        }
    }
}
