using Elasticsearch.Net;
using Nest;
using SalesProjection.Application.Abstraction;
using SalesProjection.Domain.Abstractions;

namespace SalesProjection.Infrastructure.Database.Abstractions
{
    public class DefaultRepository<T> : IDefaultRepository<T> where T : EntityBase
    {
        public readonly ElasticClient _client;
        public readonly string _indexName;

        public DefaultRepository(ElasticClient client, string indexName)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _indexName = indexName ?? throw new ArgumentNullException(nameof(indexName));
        }

        public async Task<T>? GetByIdAsync(string id)
        {         
            var response = await _client.GetAsync<T>(id, g => g.Index(_indexName));
            if (response == null) 
                return null;
            return response.Source;
        }

        public async Task<IEnumerable<T>?> GetAllAsync()
        {
            var response = await _client.SearchAsync<T>(s => s
                .Index(_indexName)
                .Query(q => q.MatchAll())
                .Size(1000) // Limite para evitar carregar todos os dados de uma vez
            );

            return response.Documents;
        }

       

        public async Task InsertAsync(T entity)
        {
            //var response = await _client.IndexDocumentAsync(entity);            
            var response = await _client.IndexAsync(entity, i => i
                .Index(_indexName) // Nome do índice
                .Id(entity.Id) // (Opcional) Especificar o ID do documento
                .Refresh(Refresh.WaitFor) // Atualiza o índice antes de responder
            );

            if (!response.IsValid)
                throw new Exception($"Erro ao inserir documento: {response.OriginalException?.Message}");
        }

        public async Task UpdateAsync(T entity, string id)
        {
            var response = await _client.UpdateAsync<T>(id, u => u
                .Index(_indexName)
                .Doc(entity)
            );

            if (!response.IsValid)
                throw new Exception($"Erro ao atualizar documento: {response.OriginalException?.Message}");
        }

        public async Task DeleteAsync(Guid id)
        {
            var response = await _client.DeleteAsync<T>(id, d => d.Index(_indexName));
            if (!response.IsValid)
                throw new Exception($"Erro ao deletar documento: {response.OriginalException?.Message}");
        }
    }
}
