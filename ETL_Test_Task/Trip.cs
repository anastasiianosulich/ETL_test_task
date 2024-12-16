namespace ETL_Test_Task;

public class Trip
{
    public DateTime TpepPickupDatetime { get; set; }
    public DateTime TpepDropoffDatetime { get; set; }
    public int? PassengerCount { get; set; }
    public double? TripDistance { get; set; }
    public string StoreAndFwdFlag { get; set; }
    public int? PULocationID { get; set; }
    public int? DOLocationID { get; set; }
    public double? FareAmount { get; set; }
    public double? TipAmount { get; set; }

    public bool hasCompleteInfo()
    {
        return PassengerCount != null && TripDistance != null && !string.IsNullOrWhiteSpace(StoreAndFwdFlag) && PULocationID != null && DOLocationID != null && FareAmount != null && TipAmount != null;
    }
}
