//==============================================================================
// FILE: Cos.cs
// ⭐⭐ DIFFICULTY: Medium | ⏱️ TIME: 25 minutes
// 🧪 TEST: cos 60 (degrees) → 0.5 | cos 0 → 1.0
//==============================================================================

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
    /// STUDENT TODO: Implement cosine (nearly identical to Sin.cs).
    /// 
    /// STEPS:
    /// 1. Validate args.Length == 1
    /// 2. Get angle from args[0]
    /// 3. IF mode is Degrees: Convert to radians (angle * Math.PI / 180.0)
    /// 4. Call Math.Cos(angle)
    /// 
    /// HINT: This is almost the same as Sin, just use Math.Cos instead of Math.Sin!
    /// </summary>
    public double Evaluate(double[] args, AngleMode mode)
    {
        throw new NotImplementedException("TODO: Implement cosine with angle mode conversion");
    }
}
