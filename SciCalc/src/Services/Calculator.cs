using System;
using System.Globalization;
using System.Linq;
using SciCalc.Models;

namespace SciCalc.Services;

/// <summary>
/// Main calculator engine that processes user input and executes operations.
/// This class demonstrates:
/// - ENCAPSULATION: Internal state (_angleMode) is private
/// - COMPOSITION: Uses OperationFactory to manage operations
/// - SINGLE RESPONSIBILITY: Only handles user interaction and operation dispatch
/// </summary>
public static class Calculator
{
    // Dictionary holds all available operations - created once using the Factory pattern
    private static readonly Dictionary<string, IOperation> Ops = OperationFactory.CreateAll();
    
    // Current angle mode setting (can be changed by user with 'mode' command)
    private static AngleMode _angleMode = AngleMode.Radians;

    //==========================================================================
    // STUDENT TODO: Implement main calculator command loop
    // ⭐⭐⭐⭐ DIFFICULTY: Hard | ⏱️ TIME: 60-90 minutes
    //
    // LEARNING OBJECTIVES:
    // - Command parsing and dispatch pattern
    // - Exception handling and error messages
    // - Pattern matching (switch expressions)
    // - REPL (Read-Eval-Print Loop) architecture
    //
    // REQUIRED STEPS:
    // 1. Print welcome message
    // 2. Infinite loop: Read line, parse command, execute, print result
    // 3. Handle special commands: 'exit', 'help', 'mode deg|rad'
    // 4. Look up operation in Ops dictionary
    // 5. Parse arguments using ParseNumber helper
    // 6. Dispatch to ITrigonometric or IOperation
    // 7. Print result with culture-invariant formatting
    // 8. Catch exceptions and print error messages
    //
    // HINTS:
    // - Use Console.ReadLine() in infinite loop (for (;;) {...})
    // - Split input: var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
    // - Look up operation: if (!Ops.TryGetValue(cmd, out var op)) { error }
    // - Use pattern matching: result = op switch { ITrigonometric trig => trig.Evaluate(args, _angleMode), _ => op.Evaluate(args) }
    // - Format output: result.ToString("G17", CultureInfo.InvariantCulture)
    //==========================================================================
    /// <summary>
    /// Main calculator loop - reads commands and executes them until user types 'exit'
    /// </summary>
    public static void Run()
    {
        throw new NotImplementedException("TODO: Implement REPL command loop");
    }

    /// <summary>
    /// Parses a string to a double number.
    /// Uses InvariantCulture to ensure '.' is always the decimal separator.
    /// </summary>
    private static double ParseNumber(string s)
    {
        if (double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out var d))
            return d;
        throw new ArgumentException($"Not a number: '{s}'");
    }

    /// <summary>
    /// Displays all available commands and help information.
    /// </summary>
    private static void PrintHelp()
    {
        Console.WriteLine("\n=== Available Operations ===");
        
        // Group operations by category for better readability
        var basic = new[] { "add", "sub", "mul", "div" };
        var trig = new[] { "sin", "cos", "tan" };
        var advanced = new[] { "sqrt", "pow", "log10", "ln", "mod", "idiv", "fact" };
        
        Console.WriteLine("\nBasic Arithmetic:");
        foreach (var name in basic)
            if (Ops.TryGetValue(name, out var op))
                Console.WriteLine($"  {op.Name,-10} : {op.Help}");
        
        Console.WriteLine("\nTrigonometric (respects angle mode):");
        foreach (var name in trig)
            if (Ops.TryGetValue(name, out var op))
                Console.WriteLine($"  {op.Name,-10} : {op.Help}");
        
        Console.WriteLine("\nAdvanced Operations:");
        foreach (var name in advanced)
            if (Ops.TryGetValue(name, out var op))
                Console.WriteLine($"  {op.Name,-10} : {op.Help}");
        
        Console.WriteLine("\n=== System Commands ===");
        Console.WriteLine("  mode       : mode deg|rad (change angle unit)");
        Console.WriteLine("  help       : Show this message");
        Console.WriteLine("  exit       : Quit calculator");
        
        Console.WriteLine("\n=== Examples ===");
        Console.WriteLine("  add 2.5 1 -2 24.5125 0.33");
        Console.WriteLine("  mode deg");
        Console.WriteLine("  sin 30");
        Console.WriteLine("  pow 2 8");
        Console.WriteLine();
    }
}
