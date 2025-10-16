using TrafficSim.Models;
using System;
using System.Linq;

namespace TrafficSim.Services;
/// <summary>
/// Minimal CLI driver for the traffic simulation.
/// </summary>
public static class App
{
    private static readonly City city = new City(3,3);

    public static void Run()
    {
        Console.WriteLine("TrafficSim — commands: path x1 y1 x2 y2 | exit");
        for (;;)
        {
            Console.Write("> ");
            var line = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(line)) continue;
            if (line.Equals("exit", StringComparison.OrdinalIgnoreCase)) return;
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            switch (parts[0])
            {
                case "path":
                    if (parts.Length!=5) { Console.WriteLine("Usage: path x1 y1 x2 y2"); break; }
                    var s = (int.Parse(parts[1]), int.Parse(parts[2]));
                    var g = (int.Parse(parts[3]), int.Parse(parts[4]));
                    var p = ShortestPath.Dijkstra(city, s, g);
                    Console.WriteLine(p.Count==0 ? "No path" :
                        string.Join(" -> ", p.Select(t=>$"({t.Item1},{t.Item2})")));
                    break;
                default:
                    Console.WriteLine("Unknown command.");
                    break;
            }
        }
    }
}
