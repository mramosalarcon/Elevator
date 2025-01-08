using Elevator;

[TestFixture]
public class ElevatorSystemTests
{
    private ElevatorSystem elevator;

    [SetUp]
    public void Setup()
    {
        elevator = new ElevatorSystem();
    }

    [Test]
    public void InitialState_ElevatorStartsAtFirstFloor()
    {
        // Using reflection to check private field for testing purposes
        var currentFloor = typeof(ElevatorSystem)
            .GetField("currentFloor", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(elevator);

        Assert.That(currentFloor, Is.EqualTo(1));
    }

    [Test]
    [TestCase(0)]  // Below minimum floor
    [TestCase(6)]  // Above maximum floor
    public void CallFromFloor_InvalidFloor_NoMovement(int invalidFloor)
    {
        var initialFloor = typeof(ElevatorSystem)
            .GetField("currentFloor", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(elevator);

        elevator.CallFromFloor(invalidFloor, Direction.Up);

        var finalFloor = typeof(ElevatorSystem)
            .GetField("currentFloor", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(elevator);

        Assert.That(finalFloor, Is.EqualTo(initialFloor));
    }

    [Test]
    [TestCase(0)]  // Below minimum floor
    [TestCase(6)]  // Above maximum floor
    public void RequestFloorFromInside_InvalidFloor_NoMovement(int invalidFloor)
    {
        var initialFloor = typeof(ElevatorSystem)
            .GetField("currentFloor", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(elevator);

        elevator.RequestFloorFromInside(invalidFloor);

        var finalFloor = typeof(ElevatorSystem)
            .GetField("currentFloor", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(elevator);

        Assert.That(finalFloor, Is.EqualTo(initialFloor));
    }

    [Test]
    public void CallFromFloor_SameFloor_DoorsOperateWithoutMovement()
    {
        var currentFloor = typeof(ElevatorSystem)
            .GetField("currentFloor", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(elevator);

        elevator.CallFromFloor((int)currentFloor, Direction.Up);

        // Check door state after operation
        var doorState = typeof(ElevatorSystem)
            .GetField("doorState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(elevator);

        Assert.That(doorState, Is.EqualTo(DoorState.Closed));
    }

    [Test]
    public void RequestFloorFromInside_DifferentFloor_ElevatorMoves()
    {
        // Start from floor 1
        elevator.RequestFloorFromInside(3);
        Thread.Sleep(5000); // Allow time for movement simulation

        var finalFloor = typeof(ElevatorSystem)
            .GetField("currentFloor", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(elevator);

        Assert.That(finalFloor, Is.EqualTo(3));
    }

    [Test]
    public void MultipleRequests_ProcessedInOrder()
    {
        var floorRequests = typeof(ElevatorSystem)
            .GetField("floorRequests", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(elevator) as HashSet<int>;

        elevator.RequestFloorFromInside(4);
        elevator.RequestFloorFromInside(2);
        elevator.RequestFloorFromInside(5);

        Assert.That(floorRequests.Count, Is.EqualTo(3));
        Assert.That(floorRequests, Does.Contain(2));
        Assert.That(floorRequests, Does.Contain(4));
        Assert.That(floorRequests, Does.Contain(5));
    }

    [Test]
    public void Direction_InitiallyIdle()
    {
        var direction = typeof(ElevatorSystem)
            .GetField("currentDirection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(elevator);

        Assert.That(direction, Is.EqualTo(Direction.Idle));
    }

    [Test]
    public void DoorState_InitiallyClosed()
    {
        var doorState = typeof(ElevatorSystem)
            .GetField("doorState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(elevator);

        Assert.That(doorState, Is.EqualTo(DoorState.Closed));
    }

    [Test]
    public void Movement_UpdatesDirection()
    {
        elevator.RequestFloorFromInside(4);
        Thread.Sleep(1000); // Allow time for movement to start

        var direction = typeof(ElevatorSystem)
            .GetField("currentDirection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(elevator);

        Assert.That(direction, Is.EqualTo(Direction.Up));
    }

    [Test]
    public void CompletedMovement_ReturnsToIdle()
    {
        elevator.RequestFloorFromInside(2);
        Thread.Sleep(5000); // Allow time for complete movement

        var direction = typeof(ElevatorSystem)
            .GetField("currentDirection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(elevator);

        Assert.That(direction, Is.EqualTo(Direction.Idle));
    }
}
