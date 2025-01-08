namespace Elevator
{
    class Program
    {
        static void Main()
        {
            ElevatorSystem elevator = new ElevatorSystem();

            // Simulate some elevator operations
            Console.WriteLine("=== Elevator Simulation Start ===");

            // Someone on 4th floor wants to go down
            elevator.CallFromFloor(4, Direction.Down);

            // Someone on 2nd floor wants to go up
            elevator.CallFromFloor(2, Direction.Up);

            // Someone inside the elevator wants to go to 5th floor
            elevator.RequestFloorFromInside(5);

            Console.WriteLine("=== Elevator Simulation End ===");
        }
    }
}