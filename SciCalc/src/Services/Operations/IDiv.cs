using System;
using SciCalc.Models;

namespace SciCalc.Services.Operations;

/// <summary>
/// Integer division operation - divides and truncates to whole number.
/// Example: idiv 7 2 → 3 (not 3.5, truncates decimal part)
/// Different from regular division which returns 3.5
/// </summary>
public sealed class IDiv : IOperation
{
    public string Name => "idiv";
    public string Help => "idiv a b - Integer division (discards remainder)";
    
    /// <summary>
    /// Performs integer division by truncating the result.
    /// Math.Truncate removes the decimal part.
    /// </summary>
    public double Evaluate(params double[] args)
    {
        if (args.Length != 2)
            throw new ArgumentException("Integer division requires exactly 2 arguments");
        
        if (args[1] == 0)
            throw new DivideByZeroException("Cannot divide by zero");
        
        // Divide and truncate to get integer result
        return Math.Truncate(args[0] / args[1]);
    }
}
