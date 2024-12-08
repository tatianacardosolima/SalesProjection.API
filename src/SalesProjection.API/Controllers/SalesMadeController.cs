﻿using Microsoft.AspNetCore.Mvc;
using SalesProjection.Application.Abstraction.Made.IUseCases;
using SalesProjection.Application.Abstraction.Made.Request;

namespace SalesProjection.API.Controllers
{
    [ApiController]
    [Route("sales-made")]
    public class SalesMadeController : ControllerBase
    {
        private readonly ILoadSalesMadeUseCase _loadSalesMadeUseCase;

        public SalesMadeController(ILoadSalesMadeUseCase loadSalesMadeUseCase)
        {
            _loadSalesMadeUseCase = loadSalesMadeUseCase;
        }
        [HttpPost("upload-csv")]
        public async Task<IActionResult> UploadCsv([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("O arquivo não foi enviado ou está vazio.");
            }
            byte[] fileBytes;
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                fileBytes = memoryStream.ToArray();
            }
            var uploadRequest = new UploadCsvRequest
            {
                FileName = file.FileName,
                FileContent = fileBytes
            };

            return Ok(await _loadSalesMadeUseCase.ExecuteAsync(uploadRequest));
        }
    }
}
