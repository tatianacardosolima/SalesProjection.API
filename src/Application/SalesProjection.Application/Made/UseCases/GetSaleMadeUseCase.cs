using SalesProjection.Application.Abstraction.Made.IRepositories;
using SalesProjection.Application.Abstraction.Made.IUseCases;
using SalesProjection.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesProjection.Application.Made.UseCases
{
    public class GetSaleMadeUseCase : IGetSaleMadeUseCase
    {
        private readonly IProcessLotRepository _repository;

        public GetSaleMadeUseCase(IProcessLotRepository repository) 
        {
            _repository = repository;
        }
        public async Task<List<ProcessLot>?> ExecuteAsync()
        {
            var response = await _repository.GetAllAsync();
            return response?.ToList();
        }
    }
}
