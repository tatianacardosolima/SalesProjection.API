using SalesProjection.Application.Abstraction.Projection.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesProjection.Application.Projection.UseCases
{
    public interface IGetProjectCurrentMonthUseCase
    {
        Task<ProjectionResponse> GetProjectionAsync();

    }
}
