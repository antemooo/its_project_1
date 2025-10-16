namespace TrafficSim.Models;

/// <summary>
/// Cardinal directions for navigation on the Manhattan grid.
/// Used to identify which side of a crossroad a vehicle approaches from.
/// 
/// Convention:
/// - North: Moving upward (decreasing Y coordinate)
/// - East: Moving right (increasing X coordinate)
/// - South: Moving downward (increasing Y coordinate)
/// - West: Moving left (decreasing X coordinate)
/// </summary>
public enum Side
{
    North,
    East,
    South,
    West
}
