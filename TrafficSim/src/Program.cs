using System;
using TrafficSim.Services;

namespace TrafficSim;

/// <summary>
/// Entry point for the Manhattan Traffic Simulation.
/// Simulates vehicles moving through a grid of crossroads with traffic lights.
/// Includes shortest path routing using Dijkstra's algorithm.
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Manhattan Traffic Simulation ===");
        Console.WriteLine("Simulate vehicles, traffic lights, and routing on a grid");
        Console.WriteLine();
        
        // Start the simulation
        App.Run();
    }
}
