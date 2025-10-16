using System;
using SciCalc.Models;

namespace SciCalc.Services.Operations;

/// <summary>
/// Tangent trigonometric function.
/// Returns the ratio of opposite to adjacent sides (or sin/cos).
/// Example: tan 45° → 1
/// </summary>
public sealed class Tan : ITrigonometric
{
    public string Name => "tan";
    public string Help => "tan x - Calculates tangent of x (respects current angle mode)";
    
    public double Evaluate(params double[] args)
    {
        throw new NotSupportedException("Trigonometric functions require angle mode. Use the overload.");
    }
    
    /// <summary>
    /// Calculates tangent with angle mode consideration.
    /// Note: Tangent is undefined at 90°, 270°, etc. (returns infinity).
    /// </summary>
    public double Evaluate(double[] args, AngleMode mode)
    {
        if (args.Length != 1)
            throw new ArgumentException("Tangent requires exactly 1 argument");
        
        var angle = args[0];
        
        // Convert degrees to radians if necessary
        if (mode == AngleMode.Degrees)
            angle = angle * Math.PI / 180.0;
        
        return Math.Tan(angle);
    }
}
