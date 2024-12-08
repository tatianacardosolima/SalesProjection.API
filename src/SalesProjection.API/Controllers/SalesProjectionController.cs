using Microsoft.AspNetCore.Mvc;
using SalesProjection.Application.Projection.UseCases;

namespace SalesProjection.API.Controllers
{
    [ApiController]
    [Route("sales-projection")]
    public class SalesProjectionController : ControllerBase
    {        
        private readonly IGetProjectCurrentMonthUseCase _getProjectCurrentMonthUseCase;

        public SalesProjectionController(IGetProjectCurrentMonthUseCase getProjectCurrentMonthUseCase)
        {
            _getProjectCurrentMonthUseCase = getProjectCurrentMonthUseCase;
        }
        [HttpGet()]
        public async Task<IActionResult> GetProject()
        {            
            return Ok(await _getProjectCurrentMonthUseCase.GetProjectionAsync());
        }
    }
}
