public class TurnLeft : ICommand
{
    // Reference to the receiver (bike controller)
    private BikeController _controller;

    // Constructor receives the bike controller
    public TurnLeft(BikeController controller)
    {
        _controller = controller;
    }

    // Execute the turn left action
    public void Execute()
    {
        _controller.Turn(Direction.Left);
    }
}
