using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using Hackathon_Simulator.dto;

namespace Hackathon_Simulator.services.impl;

public class CsvEmployeeProvider : IEmployeeProvider
{
    private readonly CsvConfiguration _configuration = new(CultureInfo.InvariantCulture)
    {
        Delimiter = ",",
        Encoding = Encoding.UTF8
    };

    public string Delimiter
    {
        get => _configuration.Delimiter;
        set => _configuration.Delimiter = value;
    }

    public Encoding Encoding
    {
        get => _configuration.Encoding;
        set => _configuration.Encoding = value;
    }

    public List<Employee> Provide(string filePath)
    {
        using var fs = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        using var textReader = new StreamReader(fs, _configuration.Encoding);
        using var csvReader = new CsvReader(textReader, _configuration);

        return csvReader.GetRecords<Employee>().ToList();
    }
}
