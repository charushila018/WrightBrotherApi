public class Flight
{
    public int Id { get; set; }
    public string FlightNumber { get; set; }
    public string Origin { get; set; }
    public string Destination { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public FlightStatus Status { get; set; }
    public int FuelRange { get; set; }

    public bool FuelTankLeak { get; set; }

    public string FlightLog => FlightLogSignature;

    // Format: DDMMYYYY-DEP-ARR-FLIGHT
    // Example: 17121903-DEP-ARR-WB001
    // For this flight
    // 17th of December 1903
    // Departure from Kitty Hawk, NC
    // Arrival at Manteo, NC
    // Flight number WB001
    public string FlightLogSignature { get; set; }

    // Encodes a series of aerobatic maneuvers
    // L = Loop, H = Hammerhead, R = Roll, S = Spin, T = Tailslide
    // Number represents repeat count, Letter represents difficulty (A-E), and each has a unique ID
    // Example 1: L4B-H2C-R3A-S1D-T2E
    // Example 2: L1A-H1B-R1C-S1D-T1E
    public string AerobaticSequenceSignature { get; set; }
}

public enum FlightStatus
{
    Scheduled,
    Boarding,
    Departed,
    InAir,
    Landed,
    Cancelled,
    Delayed
}
