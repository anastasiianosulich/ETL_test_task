namespace ETL_Test_Task;

public class Program
{
    static async Task Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: YourApp <inputFilePath> <connectionString> (for master database)");
            return;
        }

        // Get the input file path and connection string from the command-line arguments
        string inputFilePath = args[0];
        string connectionString = args[1];

        const string duplicatesFilePath = "./duplicates.csv";

        Console.WriteLine("Starting ETL process...");

        var allTripRecords = CsvUtility.ReadCsv<Trip>(inputFilePath, new TripMap());

        var (uniqueTrips, duplicateTrips) = TripUtility.FilterTrips(allTripRecords);

        if (duplicateTrips.Count != 0)
        {
            CsvUtility.WriteCsv(duplicatesFilePath, duplicateTrips);
        }

        string[] scripts = { "./SqlScripts/CreateTripsDatabase.sql", "./SqlScripts/CreateTripsTable.sql" };

        try
        {
            // Execute the scripts to create Trips db and table
            SqlUtility.ExecuteSqlScripts(connectionString, scripts);
            Console.WriteLine("Database and table creation script executed successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return;
        }

        // Insert data into the database
        var tripsDataTable = TripUtility.GetTripsDataTable(uniqueTrips);
        await TripUtility.SqlBulkInsertTrips(connectionString, tripsDataTable);

        SqlUtility.ExecuteSqlScripts(connectionString, ["./SqlScripts/CreateIndexesForTripsTable.sql"]);

        Console.WriteLine("ETL process completed.");
    }
}
