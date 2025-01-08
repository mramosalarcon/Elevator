# Elevator Control System

## Description
This is a C# implementation of an elevator control system that simulates the operation of a five-story building elevator. The system manages elevator movement, door operations, and handles both internal and external call requests.

## Features
- Five-floor operation (floors 1-5)
- External call buttons:
  - First and fifth floors: Single call button
  - Second through fourth floors: Up/down call buttons
- Internal floor selection buttons (1-5)
- Real-time elevator position tracking
- Door state management
- Direction management (Up/Down/Idle)
- Request queue handling
- Movement simulation

## Technical Specifications
- Language: C# (.NET)
- Testing Framework: NUnit
- Architecture: Object-Oriented Design

## Project Structure
```markdown
Elevator/
├── README.md
├── Elevator/
│   ├── ElevatorSystem.cs      # Main elevator control logic
│   ├── Program.cs             # Execution program
|   └── Elevator.csproj
├── TestElevator/
|   ├── ElevatorSystemTests.cs # NUnit test suite
|   └── TestElevator.csproj
└── Elevator.sln
```

## Getting Started
### Installation

  1. Clone the repository
  2. Open the solution in Visual Studio or your preferred IDE
  3. Restore NuGet packages
  4. Build the solution

### Usage Example
```C#
// Create a new elevator instance
ElevatorSystem elevator = new ElevatorSystem();

// Call elevator from a floor
elevator.CallFromFloor(4, Direction.Down);

// Request a floor from inside the elevator
elevator.RequestFloorFromInside(5);
```

## Testing
The solution includes comprehensive NUnit tests covering:
- Initial state verification
- Invalid input handling
- Movement operations
- Door operations
- Multiple request handling
- Direction changes
- State transitions

To run tests:
  1. Open Test Explorer in Visual Studio
  2. Click "Run All Tests"

## System Overview
### Core Components
  1. ElevatorSystem Class
     - Manages elevator state
     - Handles request processing
     - Controls movement and door operations
  2. Direction Enum
     - Up
     - Down
     - Idle
  3. DoorState Enum
     - Open
     - Closed

### Key Methods
- CallFromFloor(int floor, Direction direction)
- RequestFloorFromInside(int targetFloor)
- ProcessRequests()
- MoveTo(int targetFloor)
- OpenDoors()
- CloseDoors()

## Current Limitations
  - No emergency stop functionality
  - No door sensors
  - No weight limits
  - No maintenance mode
  - No door obstruction detection

## Roadmap
Future enhancements planned:
  - Advanced scheduling algorithms
  - Weight limit implementation
  - Emergency controls
  - Door sensors and obstruction detection
  - Maintenance mode
  - Logging system
  - Enhanced user interface
  - Real-time monitoring
  - Performance metrics

## Contributing
  1. Fork the repository
  2. Create a feature branch
  3. Commit your changes
  4. Push to the branch
  5. Create a Pull Request

## Known Issues
  - Timing-based tests may be sensitive to system load
  - Simulation delays may need adjustment based on requirements

## Version History
  - 1.0.0: Initial release
      - Basic elevator functionality
      - Test suite implementation
      - Documentation
   
  
