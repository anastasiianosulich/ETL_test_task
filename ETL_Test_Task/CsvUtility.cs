using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace ETL_Test_Task;

public static class CsvUtility
{
    public static void WriteCsv<T>(string filePath, List<T> records)
    {
        using (var writer = new StreamWriter(filePath))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(records);
        }
    }

    public static List<T> ReadCsv<T>(string filePath, ClassMap map)
    {
        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            csv.Context.RegisterClassMap(map);
            return csv.GetRecords<T>().ToList();
        }
    }
}