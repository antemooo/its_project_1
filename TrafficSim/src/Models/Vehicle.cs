using System;
using System.Collections.Generic;

namespace TrafficSim.Models;

/// <summary>
/// Represents a vehicle traveling through the city.
/// Tracks its journey, destination, and whether it has priority.
/// 
/// Priority vehicles (ambulances, fire trucks, police) can:
/// - Pass through red lights
/// - Move to front of queues
/// </summary>
public sealed class Vehicle
{
    /// <summary>
    /// Unique name or ID for this vehicle
    /// </summary>
    public string Name { get; }
    
    /// <summary>
    /// Grid coordinates where vehicle is trying to reach
    /// </summary>
    public (int X, int Y) Destination { get; }
    
    /// <summary>
    /// Whether this is a priority vehicle (emergency services)
    /// </summary>
    public bool Priority { get; }
    
    /// <summary>
    /// History of crossroads visited (for statistics)
    /// </summary>
    public List<(int X, int Y)> Route { get; } = new();
    
    /// <summary>
    /// Total time spent traveling (in minutes)
    /// </summary>
    public int TravelTime { get; private set; }
    
    /// <summary>
    /// Creates a new vehicle.
    /// </summary>
    /// <param name="name">Vehicle identifier</param>
    /// <param name="dest">Destination coordinates</param>
    /// <param name="priority">Is this an emergency vehicle?</param>
    public Vehicle(string name, (int X, int Y) dest, bool priority = false)
    {
        Name = name;
        Destination = dest;
        Priority = priority;
    }
    
    /// <summary>
    /// Records that vehicle passed through a crossroad.
    /// </summary>
    public void AddHop((int X, int Y) crossroad)
    {
        Route.Add(crossroad);
    }
    
    /// <summary>
    /// Increments travel time (called each simulation tick).
    /// </summary>
    public void Tick()
    {
        TravelTime += 1;
    }
    
    /// <summary>
    /// String representation showing priority status and destination.
    /// </summary>
    public override string ToString()
    {
        var prefix = Priority ? "[PRIORITY] " : "";
        return $"{prefix}{Name} → ({Destination.X},{Destination.Y})";
    }
}
