using System.Collections.Generic;

/// <summary>
/// Singleton stuff so that the siphons can look up each target zone. It provides a nicer API.
/// </summary>
public class AirZoneManager
{
    public static Dictionary<string, AirZone> AirLevels = new Dictionary<string, AirZone>();
}