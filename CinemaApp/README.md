# Cinema Application — Console (C#/.NET 8)

## 1) Purpose
Manage rooms, films and shows; book seats; list a day’s schedule. Implement a **Marathon Ticket**: an ordered set of **non‑overlapping** shows for a given day (“movie after movie”).

## 2) Learning goals
- OOP modelling: `Movie`, `Room`, `Show`, bookings.
- LINQ filters and ordering for schedules.
- Strategy for marathon planning; repository for shows.

## 3) CLI
- `add-room "Room A" 100`
- `add-movie "Inception" 02:28` (HH:mm duration)
- `add-show "Inception" "Room A" 2025-11-01T10:00 9.99`
- `list-shows 2025-11-01`
- `book <showId> 12 13 14`
- `marathon 2025-11-01`
- `exit`

## 4) Behaviour & rules
- Seats are 1..Capacity. No double‑booking. Invalid seats are rejected.
- Shows may be in different rooms; time overlaps are only about **time**, not room.
- **Marathon planner (greedy):** sort by **End**, pick next show with `Start >= lastEnd`.
  - Stretch: maximise **total watch time** (weighted variant).

## 5) Design
- `Show` exposes `Start`, `End`, `TryBook(seats[])` and a readable `ToString()`.
- `IShowStore` (in‑memory to start; swap to JSON later if you wish).
- `IMarathonPlanner` with a default greedy implementation.

## 6) Run
```bash
  dotnet new console -n CinemaApp && cd CinemaApp
  # Add CinemaApp.cs from this template (or replace Program.cs)
  dotnet run --project CinemaApp.csproj
```

## 7) One‑week plan
- **D1:** rooms/movies/shows + add/list.
- **D2:** booking with validation.
- **D3:** marathon planner.
- **D4:** optional JSON store + UX polish.
- **D5:** README examples + demo.

## 8) Acceptance checks
- Booking a taken seat fails; invalid seat number fails.
- `list-shows <date>` orders by **start time**.
- `marathon <date>` returns a non‑overlapping sequence (may span rooms).
