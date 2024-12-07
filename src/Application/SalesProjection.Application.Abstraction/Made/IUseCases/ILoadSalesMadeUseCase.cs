using SalesProjection.Application.Abstraction.Made.Request;
using SalesProjection.Application.Abstraction.Made.Responses;
using SalesProjection.Domain.Responses;

namespace SalesProjection.Application.Abstraction.Made.IUseCases
{
    public interface ILoadSalesMadeUseCase
    {
        Task<DefaultResponse<UploadCsvResponse>> ExecuteAsync(UploadCsvRequest csvRequest);
    }
}
