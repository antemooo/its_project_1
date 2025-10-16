//==============================================================================
// FILE: Street.cs
// ⭐⭐ DIFFICULTY: Medium | ⏱️ TIME: 30-40 minutes
// 🧪 TEST: Create street, TryEnter vehicles, Tick to move them, check Load
//==============================================================================

using TrafficSim.Models;
using System.Collections.Generic;

namespace TrafficSim.Services;

/// <summary>
/// Represents a street connecting two crossroads. Handles vehicle movement and capacity.
/// Vehicles spend TravelTime minutes moving through the street.
/// </summary>
public sealed class Street
{
    public bool Closed { get; set; }
    public int TravelTime { get; }
    public int Capacity { get; set; } = 100;
    
    //==========================================================================
    // STUDENT TODO: Implement Street with vehicle queue and travel time tracking
    //
    // REQUIRED FIELDS:
    // - private readonly Queue<(Vehicle v, int remaining)> _moving = new();
    //   Stores vehicles and their remaining travel time
    //
    // CONSTRUCTOR:
    // - public Street(int travelTime) { TravelTime = travelTime; }
    //
    // TRYENTER METHOD: (Add vehicle to street)
    // - Check if Closed or _moving.Count >= Capacity → return false
    // - Enqueue: _moving.Enqueue((v, TravelTime)) → return true
    //
    // TICK METHOD: (Move vehicles through street)
    // - For each vehicle in queue:
    //   1. Dequeue (v, remaining)
    //   2. Call v.Tick() (increment vehicle's travel time)
    //   3. Decrement remaining--
    //   4. If remaining <= 0: yield return v (vehicle exits)
    //   5. Else: re-enqueue with updated remaining time
    //
    // LOAD PROPERTY:
    // - public int Load => _moving.Count;
    //
    // HINTS:
    // - Use Queue<T> for FIFO vehicle processing
    // - Tuple (Vehicle, int) tracks vehicle and time left in street
    // - yield return allows streaming vehicles as they exit
    //==========================================================================
    
    private readonly Queue<(Vehicle v, int remaining)> _moving = new();
    
    public Street(int travelTime)
    {
        throw new NotImplementedException("TODO: Initialize TravelTime property");
    }

    public bool TryEnter(Vehicle v)
    {
        throw new NotImplementedException("TODO: Check capacity and closed status, enqueue vehicle");
    }

    public IEnumerable<Vehicle> Tick()
    {
        throw new NotImplementedException("TODO: Process each vehicle, decrement timer, yield those ready to exit");
    }

    public int Load => throw new NotImplementedException("TODO: Return count of vehicles in queue");
}
