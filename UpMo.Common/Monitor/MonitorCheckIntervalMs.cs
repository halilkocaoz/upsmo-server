namespace UpMo.Common.Monitor
{
    /// <summary>
    /// Check interval as miliseconds
    /// </summary>
    public enum MonitorCheckIntervalMs : uint
    {
        OneMin = 60000,
        FiveMins = OneMin * 5,
        TenMins = OneMin * 10,
        TwentyMins = OneMin * 20,
        ThirtyMins = OneMin * 30,
        FourtyMins = OneMin * 40,
        FiftyMins = OneMin * 50,
        OneHour = OneMin * 60,
        TwoHours = OneHour * 2,
        OneDay = OneHour * 24
    }
}
