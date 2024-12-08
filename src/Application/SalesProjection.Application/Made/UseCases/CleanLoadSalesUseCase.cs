using SalesProjection.Application.Abstraction.Made.IRepositories;
using SalesProjection.Application.Abstraction.Made.IUseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesProjection.Application.Made.UseCases
{
    public class CleanLoadSalesUseCase : ICleanLoadSalesUseCase
    {
        private IProcessLotRepository _repository;

        public CleanLoadSalesUseCase(IProcessLotRepository repository)
        {
            _repository = repository;
        }
        public async Task ExecuteAsync()
        {
            var list = await _repository.GetAllAsync();
            if (list != null)
            {
                foreach (var item in list)
                {
                    await _repository.DeleteAsync(item.Id);
                }
            }
        }
    }
}
