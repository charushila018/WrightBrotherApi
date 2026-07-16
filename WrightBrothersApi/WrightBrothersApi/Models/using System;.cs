using System;
using System.Globalization;

namespace WrightBrothersApi.Models;

public record FlightLog(DateTime Date, string DepartureCode, string ArrivalCode, string FlightNumber)
{
    public static FlightLog Parse(string signature)
    {
        if (string.IsNullOrWhiteSpace(signature))
        {
            throw new ArgumentException("Flight log signature cannot be empty.", nameof(signature));
        }

        var parts = signature.Split('-');
        if (parts.Length != 4)
        {
            throw new FormatException("Expected format: DDMMYYYY-DEP-ARR-FLIGHT");
        }

        if (!DateTime.TryParseExact(
                parts[0],
                "ddMMyyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var date))
        {
            throw new FormatException("Invalid date segment. Expected DDMMYYYY.");
        }

        return new FlightLog(date, parts[1], parts[2], parts[3]);
    }

    public override string ToString()
        => $"{Date:ddMMyyyy}-{DepartureCode}-{ArrivalCode}-{FlightNumber}";
}