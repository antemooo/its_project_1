//==============================================================================
// FILE: ShortestPath.cs
// ⭐⭐⭐⭐⭐ DIFFICULTY: Very Hard | ⏱️ TIME: 90-120 minutes
// 🧪 TEST: Dijkstra on 3x3 grid, (0,0) → (2,2), expect path of length 5
//==============================================================================

using TrafficSim.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrafficSim.Services;

/// <summary>
/// Implements Dijkstra's shortest path algorithm for the city grid.
/// 
/// ALGORITHM (Classic Dijkstra):
/// 1. Initialize all distances to infinity, except start (distance 0)
/// 2. Create unvisited set Q with all nodes
/// 3. While Q not empty:
///    a. Pick node u with minimum distance from Q
///    b. Remove u from Q
///    c. If u is goal, break (found shortest path)
///    d. For each neighbor of u:
///       - Calculate alternative distance: dist[u] + edge_weight
///       - If alternative < current distance: update distance and predecessor
/// 4. Reconstruct path by backtracking from goal through predecessors
/// 
/// Time Complexity: O((V + E) log V) with priority queue, O(V²) with linear search
/// Space Complexity: O(V) for distance and predecessor maps
/// </summary>
public static class ShortestPath
{
    //==========================================================================
    // STUDENT TODO: Implement Dijkstra's shortest path algorithm
    //
    // LEARNING OBJECTIVES:
    // - Understand graph algorithms and pathfinding
    // - Implement classic Dijkstra algorithm
    // - Work with priority queues and graph traversal
    // - Practice path reconstruction
    //
    // DATA STRUCTURES NEEDED:
    // - dist: Dictionary<(int, int), int> - distance from start to each node
    // - prev: Dictionary<(int, int), (int, int)?> - predecessor for path reconstruction
    // - q: HashSet<(int, int)> - unvisited nodes
    //
    // IMPLEMENTATION STEPS:
    //
    // 1. INITIALIZATION:
    //    - Create empty dictionaries: dist, prev
    //    - Create unvisited set: q = new HashSet<(int, int)>()
    //    - For x in [0, city.W), y in [0, city.H):
    //      * dist[(x, y)] = int.MaxValue (infinity)
    //      * q.Add((x, y))
    //    - Set start: dist[start] = 0, prev[start] = null
    //
    // 2. MAIN LOOP: while (q.Count > 0)
    //    a. Find node u with minimum distance:
    //       var u = q.OrderBy(p => dist[p]).First();
    //    b. Remove u from unvisited: q.Remove(u);
    //    c. Early exit: if (u.Equals(goal)) break;
    //    d. Relax edges to neighbors:
    //       foreach (var (p, dir) in city.Neighbors(u))
    //       {
    //           int alt = dist[u] + 1;  // Edge weight = 1 (can extend to use street travel time)
    //           if (alt < dist[p])
    //           {
    //               dist[p] = alt;
    //               prev[p] = u;
    //           }
    //       }
    //
    // 3. PATH RECONSTRUCTION:
    //    - var path = new List<(int, int)>();
    //    - var cur = goal;
    //    - if (!prev.ContainsKey(cur)) return path; (no path exists)
    //    - Backtrack loop:
    //      * path.Add(cur)
    //      * if (prev[cur] is null) break; (reached start)
    //      * cur = prev[cur]!.Value;
    //    - path.Reverse(); (path was built backwards)
    //    - return path;
    //
    // HINTS:
    // - q.OrderBy(p => dist[p]).First() finds minimum (inefficient but simple)
    // - For better performance, use PriorityQueue<T, int> (.NET 6+)
    // - Edge relaxation: if new path is shorter, update distance
    // - Predecessor map allows path reconstruction
    // - Path is built backwards (goal → start), so reverse at end
    //
    // EXTENSIONS (Optional):
    // - Use street.TravelTime instead of fixed weight 1
    // - Handle closed streets (infinite weight or skip)
    // - Add traffic congestion costs
    //==========================================================================
    
    public static List<(int X, int Y)> Dijkstra(City city, (int X, int Y) start, (int X, int Y) goal)
    {
        throw new NotImplementedException("TODO: Implement Dijkstra's shortest path algorithm");
    }
}
