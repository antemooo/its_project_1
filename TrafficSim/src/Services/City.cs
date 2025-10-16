using TrafficSim.Models;
using System.Collections.Generic;

namespace TrafficSim.Services;
/// <summary>
/// Represents the city grid of crossroads.
/// </summary>
public sealed class City
{
    private readonly CrossRoad[,] _grid;
    public int W { get; }
    public int H { get; }
    public City(int w,int h)
    {
        W=w; H=h; _grid = new CrossRoad[w,h];
        for (int x=0;x<w;x++) for (int y=0;y<h;y++) _grid[x,y]=new CrossRoad(x,y);
    }
    public CrossRoad At(int x,int y) => _grid[x,y];

    // Graph neighbor helper
    public IEnumerable<((int X,int Y) P, Side Dir)> Neighbors((int X,int Y) p)
    {
        if (p.Y>0) yield return (((p.X,p.Y-1)), Side.North);
        if (p.X<W-1) yield return (((p.X+1,p.Y)), Side.East);
        if (p.Y<H-1) yield return (((p.X,p.Y+1)), Side.South);
        if (p.X>0) yield return (((p.X-1,p.Y)), Side.West);
    }
}
