//==============================================================================
// FILE: CrossRoad.cs
// ⭐⭐⭐ DIFFICULTY: Medium | ⏱️ TIME: 35-45 minutes
// 🧪 TEST: Create crossroad, access Light(Side.North), check traffic light states
//==============================================================================

using TrafficSim.Models;
using System;
using System.Collections.Generic;

namespace TrafficSim.Services;

/// <summary>
/// Represents a crossroad with traffic lights and outgoing streets.
/// Each crossroad has 4 sides (North, East, South, West), each with:
/// - A traffic light controlling incoming vehicles
/// - An outgoing street leading to the next crossroad
/// </summary>
public sealed class CrossRoad
{
    public (int X, int Y) Pos { get; }
    
    //==========================================================================
    // STUDENT TODO: Implement CrossRoad with traffic lights and streets
    //
    // REQUIRED FIELDS:
    // - private readonly Dictionary<Side, TrafficLight> _lights = new();
    // - private readonly Dictionary<Side, Street> _streetsOut = new();
    //
    // CONSTRUCTOR: public CrossRoad(int x, int y)
    // - Set Pos = (x, y)
    // - For each Side (use Enum.GetValues(typeof(Side))):
    //   * Create TrafficLight: _lights[side] = new TrafficLight(LightState.Green)
    //   * Create Street: _streetsOut[side] = new Street(travelTime: 1)
    //
    // ACCESSOR METHODS:
    // - public TrafficLight Light(Side s) => _lights[s];
    // - public Street StreetOut(Side s) => _streetsOut[s];
    //
    // TICK METHOD:
    // - Call Tick() on all traffic lights: foreach (var l in _lights.Values) l.Tick()
    //
    // TOSTRING:
    // - return $"Cross({Pos.X},{Pos.Y})";
    //
    // HINTS:
    // - Dictionary<Side, T> maps each direction to its light/street
    // - Enum.GetValues(typeof(Side)) loops through North, East, South, West
    // - Tick propagates time to all traffic lights
    //==========================================================================
    
    private readonly Dictionary<Side, TrafficLight> _lights = new();
    private readonly Dictionary<Side, Street> _streetsOut = new();

    public CrossRoad(int x, int y)
    {
        throw new NotImplementedException("TODO: Set Pos, initialize lights and streets for all 4 sides");
    }

    public TrafficLight Light(Side s)
    {
        throw new NotImplementedException("TODO: Return traffic light for given side");
    }

    public Street StreetOut(Side s)
    {
        throw new NotImplementedException("TODO: Return outgoing street for given side");
    }

    public void Tick()
    {
        throw new NotImplementedException("TODO: Call Tick() on all traffic lights");
    }

    public override string ToString()
    {
        throw new NotImplementedException("TODO: Return formatted string with position");
    }
}
