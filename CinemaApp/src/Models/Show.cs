using System;
using System.Collections.Generic;
using System.Linq;

namespace CinemaApp.Models;

/// <summary>
/// Represents a specific showing of a movie in a room at a particular time.
/// This class demonstrates:
/// - ENCAPSULATION: Private _taken set, public read-only properties
/// - DATA VALIDATION: TryBook checks seat validity and availability
/// - BUSINESS LOGIC: Manages seat booking without external dependencies
/// </summary>
public sealed class Show
{
    // Unique identifier for this show (automatically generated)
    public Guid Id { get; } = Guid.NewGuid();
    
    // What movie is being shown
    public Movie Movie { get; }
    
    // Which room is hosting this show
    public Room Room { get; }
    
    // When the show starts
    public DateTime Start { get; }
    
    // When the show ends (calculated from start + movie duration)
    public DateTime End => Start + Movie.Duration;
    
    // Ticket price for this show
    public decimal Price { get; }
    
    // Private set of booked seat numbers - uses HashSet for fast lookup
    // HashSet provides O(1) Contains operation for checking if seat is taken
    private readonly HashSet<int> _taken = new();
    
    /// <summary>
    /// Creates a new show.
    /// Constructor demonstrates COMPOSITION - Show "has-a" Movie and Room.
    /// </summary>
    public Show(Movie movie, Room room, DateTime start, decimal price)
    {
        Movie = movie;
        Room = room;
        Start = start;
        Price = price;
    }
    
    /// <summary>
    /// Attempts to book specified seats.
    /// Returns true if booking succeeds, false if any seat is invalid or taken.
    /// This method demonstrates DEFENSIVE PROGRAMMING - validates all inputs.
    /// </summary>
    /// <param name="seats">Array of seat numbers to book (1-indexed)</param>
    /// <returns>True if all seats were successfully booked, false otherwise</returns>
    public bool TryBook(params int[] seats)
    {
        // Validate input
        if (seats.Length == 0)
            return false;
        
        // Check if any seat number is out of range (1 to Capacity)
        if (seats.Any(s => s < 1 || s > Room.Capacity))
            return false;
        
        // Check if any seat is already taken
        if (seats.Any(_taken.Contains))
            return false;
        
        // All validations passed - book all seats
        foreach (var seat in seats)
        {
            _taken.Add(seat);
        }
        
        return true;
    }
    
    /// <summary>
    /// Gets the number of available seats remaining.
    /// </summary>
    public int AvailableSeats => Room.Capacity - _taken.Count;
    
    /// <summary>
    /// Provides a detailed string representation of the show.
    /// </summary>
    public override string ToString()
    {
        return $"[{Id:N}] {Movie.Title} | {Room.Name} | " +
               $"{Start:yyyy-MM-dd HH:mm} - {End:HH:mm} | " +
               $"${Price:F2} | Seats: {AvailableSeats}/{Room.Capacity} available";
    }
}
