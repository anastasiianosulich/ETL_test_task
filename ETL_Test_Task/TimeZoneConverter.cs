namespace ETL_Test_Task;

public static class TimeZoneConverter
{
    private static TimeZoneInfo _estTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

    public static DateTime ConvertESTToUTC(DateTime estDateTime)
    {
        return TimeZoneInfo.ConvertTimeToUtc(estDateTime, _estTimeZone);
    }
}
