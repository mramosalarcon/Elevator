namespace Elevator
{

    public enum Direction
    {
        Up,
        Down,
        Idle
    }

    public enum DoorState
    {
        Open,
        Closed
    }

    public class ElevatorSystem
    {
        private int currentFloor;
        private Direction currentDirection;
        private DoorState doorState;
        private HashSet<int> floorRequests;
        private const int MinFloor = 1;
        private const int MaxFloor = 5;
        private bool isMoving;

        public ElevatorSystem()
        {
            currentFloor = 1;
            currentDirection = Direction.Idle;
            doorState = DoorState.Closed;
            floorRequests = new HashSet<int>();
            isMoving = false;
        }

        public void CallFromFloor(int floor, Direction direction)
        {
            if (floor < MinFloor || floor > MaxFloor)
            {
                Console.WriteLine("Invalid floor request");
                return;
            }

            if (floor == currentFloor && !isMoving)
            {
                OpenDoors();
                return;
            }

            floorRequests.Add(floor);
            ProcessRequests();
        }

        public void RequestFloorFromInside(int targetFloor)
        {
            if (targetFloor < MinFloor || targetFloor > MaxFloor)
            {
                Console.WriteLine("Invalid floor request");
                return;
            }

            if (targetFloor == currentFloor && !isMoving)
            {
                OpenDoors();
                return;
            }

            floorRequests.Add(targetFloor);
            ProcessRequests();
        }

        private void ProcessRequests()
        {
            if (floorRequests.Count == 0)
            {
                currentDirection = Direction.Idle;
                return;
            }

            isMoving = true;

            while (floorRequests.Count > 0)
            {
                int nextFloor = GetNextFloor();
                MoveTo(nextFloor);
                floorRequests.Remove(nextFloor);
                OpenDoors();
                CloseDoors();
            }

            isMoving = false;
            currentDirection = Direction.Idle;
        }

        private int GetNextFloor()
        {
            int nextFloor = currentFloor;

            foreach (int floor in floorRequests)
            {
                if (currentDirection == Direction.Up && floor > currentFloor)
                {
                    nextFloor = (nextFloor == currentFloor) ? floor : Math.Min(nextFloor, floor);
                }
                else if (currentDirection == Direction.Down && floor < currentFloor)
                {
                    nextFloor = (nextFloor == currentFloor) ? floor : Math.Max(nextFloor, floor);
                }
                else if (currentDirection == Direction.Idle)
                {
                    nextFloor = floor;
                    currentDirection = floor > currentFloor ? Direction.Up : Direction.Down;
                }
            }

            return nextFloor;
        }

        private void MoveTo(int targetFloor)
        {
            Console.WriteLine($"Moving from floor {currentFloor} to floor {targetFloor}");

            while (currentFloor != targetFloor)
            {
                Thread.Sleep(1000); // Simulate movement time

                if (targetFloor > currentFloor)
                {
                    currentFloor++;
                    Console.WriteLine($"Passing floor {currentFloor}...");
                }
                else
                {
                    currentFloor--;
                    Console.WriteLine($"Passing floor {currentFloor}...");
                }
            }

            Console.WriteLine($"Arrived at floor {currentFloor}");
        }

        private void OpenDoors()
        {
            if (doorState == DoorState.Closed)
            {
                Console.WriteLine("Opening doors...");
                Thread.Sleep(1000); // Simulate door operation
                doorState = DoorState.Open;
                Console.WriteLine("Doors are open");
                Thread.Sleep(2000); // Keep doors open for passengers
            }
        }

        private void CloseDoors()
        {
            if (doorState == DoorState.Open)
            {
                Console.WriteLine("Closing doors...");
                Thread.Sleep(1000); // Simulate door operation
                doorState = DoorState.Closed;
                Console.WriteLine("Doors are closed");
            }
        }
    }
}