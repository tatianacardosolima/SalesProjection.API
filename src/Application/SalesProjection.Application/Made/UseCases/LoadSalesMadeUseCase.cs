using CsvHelper.Configuration;
using CsvHelper;
using SalesProjection.Application.Abstraction.Made.IRepositories;
using SalesProjection.Application.Abstraction.Made.IUseCases;
using SalesProjection.Application.Abstraction.Made.Request;
using SalesProjection.Application.Abstraction.Made.Responses;
using SalesProjection.Domain.Entities;
using SalesProjection.Domain.Exceptions;
using SalesProjection.Domain.Responses;
using System.Globalization;

namespace SalesProjection.Application.Made.UseCases
{
    public class LoadSalesMadeUseCase : ILoadSalesMadeUseCase
    {
        //public const string TEMPPATH = @"/app/temp-files";
        public const string TEMPPATH = @"C:\_code\SalesProjection.API\temp";
        private readonly IProcessLotRepository _repository;

        public LoadSalesMadeUseCase(IProcessLotRepository repository)
        {
            _repository = repository;
        }
        public async Task<DefaultResponse<UploadCsvResponse>> ExecuteAsync(UploadCsvRequest request)
        {
            var inProcessPath = $@"{TEMPPATH}\\InProcessing";
            if (!Directory.Exists(TEMPPATH))
            {
                Directory.CreateDirectory(inProcessPath);
            }

            var filePath = Path.Combine(inProcessPath, request.FileName);

            File.WriteAllBytes(filePath, request.FileContent!);


            var list = await ReadCsvFileAsync(filePath);
            var branchName = request.FileName.Split("_")[0];
            var region = request.FileName.Split("_")[1];
            var period = request.FileName.Split("_")[2].Replace(".csv", "");
            var entity = new ProcessLot(list!, region, branchName, period);

            await _repository.InsertAsync(entity);
            File.Delete(filePath);

            return new DefaultResponse<UploadCsvResponse>(new UploadCsvResponse(entity.Id) , "Aguarde o processamento do arquivo", true);
        }

        public async Task<List<SaleMadeItem>?> ReadCsvFileAsync(string filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";", // Define o delimitador
                HasHeaderRecord = true, // Se o CSV tem cabeçalho
            });
            var records = new List<SaleMadeItem>();

            // Mapeia os dados do arquivo para a classe CsvRecord
            await foreach (var record in csv.GetRecordsAsync<SaleMadeItem>())
            {
                records.Add(record);
            }

            return records;
        }
        public class CsvRecord
        {
            public DateTime Date { get; set; }
            public int IntegerValue { get; set; }
            public double DoubleValue { get; set; }
        }
    }
}
