using Microsoft.Data.SqlClient;
using System.Data;

namespace ETL_Test_Task;

public class TripUtility
{
    public static async Task SqlBulkInsertTrips(string connectionString, DataTable dataTable)
    {
        using var bulk = new SqlBulkCopy(connectionString);

        bulk.DestinationTableName = "TripsDB.dbo.Trips";

        bulk.ColumnMappings.Add(nameof(Trip.TpepPickupDatetime), nameof(Trip.TpepPickupDatetime));
        bulk.ColumnMappings.Add(nameof(Trip.TpepDropoffDatetime), nameof(Trip.TpepDropoffDatetime));
        bulk.ColumnMappings.Add(nameof(Trip.PassengerCount), nameof(Trip.PassengerCount));
        bulk.ColumnMappings.Add(nameof(Trip.TripDistance), nameof(Trip.TripDistance));
        bulk.ColumnMappings.Add(nameof(Trip.StoreAndFwdFlag), nameof(Trip.StoreAndFwdFlag));
        bulk.ColumnMappings.Add(nameof(Trip.PULocationID), nameof(Trip.PULocationID));
        bulk.ColumnMappings.Add(nameof(Trip.DOLocationID), nameof(Trip.DOLocationID));
        bulk.ColumnMappings.Add(nameof(Trip.FareAmount), nameof(Trip.FareAmount));
        bulk.ColumnMappings.Add(nameof(Trip.TipAmount), nameof(Trip.TipAmount));

        await bulk.WriteToServerAsync(dataTable);
    }

    public static DataTable GetTripsDataTable(List<Trip> trips)
    {
        var dataTable = new DataTable();

        dataTable.Columns.Add(nameof(Trip.TpepPickupDatetime), typeof(DateTime));
        dataTable.Columns.Add(nameof(Trip.TpepDropoffDatetime), typeof(DateTime));
        dataTable.Columns.Add(nameof(Trip.PassengerCount), typeof(int));
        dataTable.Columns.Add(nameof(Trip.TripDistance), typeof(double));
        dataTable.Columns.Add(nameof(Trip.StoreAndFwdFlag), typeof(string));
        dataTable.Columns.Add(nameof(Trip.PULocationID), typeof(int));
        dataTable.Columns.Add(nameof(Trip.DOLocationID), typeof(int));
        dataTable.Columns.Add(nameof(Trip.FareAmount), typeof(double));
        dataTable.Columns.Add(nameof(Trip.TipAmount), typeof(double));

        foreach (var trip in trips)
        {
            dataTable.Rows.Add(trip.TpepPickupDatetime, trip.TpepDropoffDatetime, trip.PassengerCount, trip.TripDistance, trip.StoreAndFwdFlag, trip.PULocationID, trip.DOLocationID, trip.FareAmount, trip.TipAmount);
        }

        return dataTable;
    }

    public static(List<Trip> uniqueTrips, List<Trip> duplicateTrips) FilterTrips(List<Trip> allTrips)
    {
        var uniqueTripKeys = new HashSet<string>();
        var duplicateTrips = new List<Trip>();
        var uniqueTrips = new List<Trip>();

        foreach (var trip in allTrips)
        {
            if (!trip.hasCompleteInfo())
            {
                continue;
            }

            // Clean and normalize data
            trip.StoreAndFwdFlag = trip.StoreAndFwdFlag.Trim() == "Y" ? "Yes" : "No";
            trip.TpepPickupDatetime = TimeZoneConverter.ConvertESTToUTC(trip.TpepPickupDatetime);
            trip.TpepDropoffDatetime = TimeZoneConverter.ConvertESTToUTC(trip.TpepDropoffDatetime);

            // Deduplicate based on unique key
            var key = $"{trip.TpepPickupDatetime}_{trip.TpepDropoffDatetime}_{trip.PassengerCount}";
            if (uniqueTripKeys.Contains(key))
            {
                duplicateTrips.Add(trip);
            }
            else
            {
                uniqueTripKeys.Add(key);
                uniqueTrips.Add(trip);
            }
        }

        return (uniqueTrips, duplicateTrips);
    }
}
