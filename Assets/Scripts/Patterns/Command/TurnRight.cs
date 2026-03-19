public class TurnRight : ICommand
{
    private BikeController _controller;

    public TurnRight(BikeController controller)
    {
        _controller = controller;
    }

    public void Execute()
    {
        _controller.Turn(Direction.Right);
    }
}
