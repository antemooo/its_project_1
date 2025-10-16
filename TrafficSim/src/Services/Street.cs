using TrafficSim.Models;
using System.Collections.Generic;

namespace TrafficSim.Services;
/// <summary>
/// Represents a street connecting two crossroads. Handles vehicle movement and capacity.
/// </summary>
public sealed class Street
{
    public bool Closed { get; set; }
    public int TravelTime { get; }
    public int Capacity { get; set; } = 100;
    private readonly Queue<(Vehicle v,int remaining)> _moving = new();
    public Street(int travelTime) { TravelTime = travelTime; }

    public bool TryEnter(Vehicle v)
    {
        if (Closed || _moving.Count >= Capacity) return false;
        _moving.Enqueue((v, TravelTime)); return true;
    }

    public IEnumerable<Vehicle> Tick()
    {
        int n = _moving.Count;
        for (int i=0;i<n;i++)
        {
            var (v, rem) = _moving.Dequeue();
            v.Tick();
            rem--;
            if (rem <= 0) yield return v;
            else _moving.Enqueue((v, rem));
        }
    }

    public int Load => _moving.Count;
}
