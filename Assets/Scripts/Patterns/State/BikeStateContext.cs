public class BikeStateContext
{
    // Current state
    public IBikeState CurrentState { get; set; }

    // Reference to controller
    private readonly BikeController
        _bikeController;

    public BikeStateContext(
        BikeController bikeController)
    {
        _bikeController = bikeController;
    }

    // Transition current state
    public void Transition()
    {
        CurrentState.Handle(_bikeController);
    }

    // Transition to new state
    public void Transition(IBikeState state)
    {
        CurrentState = state;
        CurrentState.Handle(_bikeController);
    }
}