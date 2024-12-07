// Caminho do arquivo a ser criado
string branch = "filia01";
DateTime start_month = new DateTime(2024, 11, 01);
DateTime end_month = new DateTime(2024, 11, 30);
string filePath = $@"C:\_code\SalesProjection.API\src\Console\Create.Load.CSV\csv\{branch}_sul_{end_month:dd-MM-yyyy}.csv";

// Conteúdo a ser escrito no arquivo
List<string> linhas = new List<string>();


try
{
    linhas.Add("Period;Hour;Sale");
    for (DateTime dataAtual = start_month; dataAtual <= end_month; dataAtual = dataAtual.AddDays(1))
    {
        for (var x = 1; x <= 24; x++)
        {
            linhas.Add($"{dataAtual:yyyy/MM/dd};{x};{Math.Round(new Random().NextDouble()*100,2)}");
        }

    }
    // Verifica se o diretório existe, caso contrário, cria
    string directory = Path.GetDirectoryName(filePath);
    if (!Directory.Exists(directory))
    {
        Directory.CreateDirectory(directory);
    }

    // Cria e escreve o arquivo texto
    File.WriteAllLines(filePath, linhas);

    Console.WriteLine($"Arquivo '{filePath}' gerado com sucesso!");
}
catch (Exception ex)
{
    Console.WriteLine($"Ocorreu um erro: {ex.Message}");
}