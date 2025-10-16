using TrafficSim.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrafficSim.Services;
/// <summary>
/// Implements Dijkstra's shortest path algorithm for the city grid.
/// </summary>
public static class ShortestPath
{
    public static List<(int X,int Y)> Dijkstra(City city, (int X,int Y) start, (int X,int Y) goal)
    {
        var dist = new Dictionary<(int,int), int>();
        var prev = new Dictionary<(int,int), (int,int)?>();
        var q = new HashSet<(int,int)>();
        for (int x=0;x<city.W;x++) for (int y=0;y<city.H;y++) { dist[(x,y)] = int.MaxValue; q.Add((x,y)); }
        dist[start] = 0; prev[start] = null;

        while (q.Count>0)
        {
            var u = q.OrderBy(p => dist[p]).First();
            q.Remove(u);
            if (u.Equals(goal)) break;
            foreach (var (p,dir) in city.Neighbors(u))
            {
                int alt = dist[u] + 1; // starter weight; extend to read street travel time
                if (alt < dist[p]) { dist[p] = alt; prev[p] = u; }
            }
        }
        // reconstruct
        var path = new List<(int,int)>();
        var cur = goal;
        if (!prev.ContainsKey(cur)) return path;
        while (true)
        {
            path.Add(cur);
            if (prev[cur] is null) break;
            cur = prev[cur]!.Value;
        }
        path.Reverse();
        return path;
    }
}
