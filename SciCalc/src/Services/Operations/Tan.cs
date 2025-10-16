//==============================================================================
// FILE: Tan.cs
// ⭐⭐ DIFFICULTY: Medium | ⏱️ TIME: 25 minutes
// 🧪 TEST: tan 45 (degrees) → 1.0 | tan 0 → 0.0
//==============================================================================

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
    /// STUDENT TODO: Implement tangent (same pattern as Sin/Cos).
    /// 
    /// STEPS:
    /// 1. Validate args.Length == 1
    /// 2. Get angle from args[0]
    /// 3. IF mode is Degrees: Convert to radians (angle * Math.PI / 180.0)
    /// 4. Call Math.Tan(angle)
    /// 
    /// NOTE: tan(90°) and tan(270°) are undefined (return infinity)
    /// HINT: tan(x) = sin(x) / cos(x), but just use Math.Tan for simplicity
    /// </summary>
    public double Evaluate(double[] args, AngleMode mode)
    {
        throw new NotImplementedException("TODO: Implement tangent with angle mode conversion");
    }
}
