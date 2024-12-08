using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesProjection.Application.Abstraction.Made.IUseCases
{
    public interface ICleanLoadSalesUseCase
    {
        Task ExecuteAsync();
    }
}
