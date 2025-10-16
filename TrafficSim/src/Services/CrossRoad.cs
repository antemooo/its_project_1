using TrafficSim.Models;
using System;
using System.Collections.Generic;

namespace TrafficSim.Services;
/// <summary>
/// Represents a crossroad with traffic lights and outgoing streets.
/// </summary>
public sealed class CrossRoad
{
    public (int X,int Y) Pos { get; }
    private readonly Dictionary<Side, TrafficLight> _lights = new();
    private readonly Dictionary<Side, Street> _streetsOut = new();

    public CrossRoad(int x, int y)
    {
        Pos=(x,y);
        foreach (Side s in Enum.GetValues(typeof(Side)))
        {
            _lights[s] = new TrafficLight(LightState.Green);
            _streetsOut[s] = new Street(travelTime:1);
        }
    }

    public TrafficLight Light(Side s) => _lights[s];
    public Street StreetOut(Side s) => _streetsOut[s];

    public void Tick()
    {
        foreach (var l in _lights.Values) l.Tick();
    }

    public override string ToString() => $"Cross({Pos.X},{Pos.Y})";
}
