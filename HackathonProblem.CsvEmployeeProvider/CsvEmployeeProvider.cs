﻿using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using HackathonProblem.Contracts.dto;
using HackathonProblem.Contracts.services;

namespace HackathonProblem.CsvEmployeeProvider;

public class CsvEmployeeProvider(string delimiter, Encoding encoding) : IEmployeeProvider
{
    private readonly CsvConfiguration _configuration = new(CultureInfo.InvariantCulture)
    {
        Delimiter = delimiter,
        Encoding = encoding
    };

    public List<Employee> Provide(string filePath)
    {
        try
        {
            using var fs = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var textReader = new StreamReader(fs, _configuration.Encoding);
            using var csvReader = new CsvReader(textReader, _configuration);

            return csvReader.GetRecords<Employee>().ToList();
        }
        catch (Exception exception)
        {
            throw new CsvEmployeeProviderException("Providing failed", exception);
        }
    }
}
