//==============================================================================
// FILE: Sin.cs
// ⭐⭐ DIFFICULTY: Medium | ⏱️ TIME: 25 minutes
// 🧪 TEST: sin 30 (degrees) → 0.5 | sin π/2 (radians) → 1.0
//==============================================================================

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
    /// STUDENT TODO: Implement sine with angle conversion.
    /// 
    /// STEPS:
    /// 1. Validate args.Length == 1
    /// 2. Get angle from args[0]
    /// 3. IF mode is Degrees: Convert to radians (angle * Math.PI / 180.0)
    /// 4. Call Math.Sin(angle) - it expects radians
    /// 
    /// HINT: Formula to convert degrees→radians: radians = degrees × (π / 180)
    /// </summary>
    public double Evaluate(double[] args, AngleMode mode)
    {
        throw new NotImplementedException("TODO: Implement sine with angle mode conversion");
    }
}
