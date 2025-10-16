using System;
using SciCalc.Models;

namespace SciCalc.Services.Operations;

/// <summary>
/// Sine trigonometric function.
/// Returns the ratio of opposite side to hypotenuse in a right triangle.
/// Example: sin 30° → 0.5
/// </summary>
public sealed class Sin : ITrigonometric
{
    public string Name => "sin";
    public string Help => "sin x - Calculates sine of x (respects current angle mode)";
    
    /// <summary>
    /// Standard Evaluate throws exception - trigonometric operations must use angle mode.
    /// </summary>
    public double Evaluate(params double[] args)
    {
        throw new NotSupportedException("Trigonometric functions require angle mode. Use the overload.");
    }
    
    /// <summary>
    /// Calculates sine with angle mode consideration.
    /// Math.Sin expects radians, so we convert degrees if needed.
    /// </summary>
    public double Evaluate(double[] args, AngleMode mode)
    {
        if (args.Length != 1)
            throw new ArgumentException("Sine requires exactly 1 argument");
        
        var angle = args[0];
        
        // Convert degrees to radians if necessary
        // Formula: radians = degrees × (π / 180)
        if (mode == AngleMode.Degrees)
            angle = angle * Math.PI / 180.0;
        
        return Math.Sin(angle);
    }
}
