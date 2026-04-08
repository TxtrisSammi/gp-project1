public class BikeStateContext
{
    // Current state
    public IBikeState CurrentState { get; set; }

    // Reference to controller
    private readonly BikeController _bikeController;

    // Keep track of crashes
    private bool _isCrashed = false;
    
    public BikeStateContext(BikeController bikeController)
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
        
        if (!_isCrashed)
            CurrentState.Handle(_bikeController);
        if (CurrentState.IsCrashed())
            _isCrashed = !_isCrashed;
    }
}