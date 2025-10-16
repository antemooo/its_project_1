using System;
using SciCalc.Models;

namespace SciCalc.Services.Operations;

/// <summary>
/// Cosine trigonometric function.
/// Returns the ratio of adjacent side to hypotenuse in a right triangle.
/// Example: cos 60° → 0.5
/// </summary>
public sealed class Cos : ITrigonometric
{
    public string Name => "cos";
    public string Help => "cos x - Calculates cosine of x (respects current angle mode)";
    
    public double Evaluate(params double[] args)
    {
        throw new NotSupportedException("Trigonometric functions require angle mode. Use the overload.");
    }
    
    /// <summary>
    /// Calculates cosine with angle mode consideration.
    /// </summary>
    public double Evaluate(double[] args, AngleMode mode)
    {
        if (args.Length != 1)
            throw new ArgumentException("Cosine requires exactly 1 argument");
        
        var angle = args[0];
        
        // Convert degrees to radians if necessary
        if (mode == AngleMode.Degrees)
            angle = angle * Math.PI / 180.0;
        
        return Math.Cos(angle);
    }
}
