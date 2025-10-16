using System;
using SciCalc.Services;

namespace SciCalc;

/// <summary>
/// Entry point for the Scientific Calculator application.
/// This class initializes and starts the calculator.
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Scientific Calculator ===");
        Console.WriteLine("A console-based calculator with basic and advanced operations");
        Console.WriteLine();
        
        // Start the calculator - all logic is in the Calculator class
        Calculator.Run();
    }
}
