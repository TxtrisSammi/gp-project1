using UnityEngine;

public class BikeController : MonoBehaviour
{
    // Configuration
    public float maxSpeed = 2.0f;

    public float turnDistance = 2.0f;

    // Current state data

    public float CurrentSpeed { get; set; }

    public Direction CurrentTurnDirection { get; private set; }

    // State References

    private IBikeState _startState,
        _stopState,
        _turnState;

    private BikeStateContext _bikeStateContext;

    void Start()

    {
        _bikeStateContext = new BikeStateContext(this);

        _startState = gameObject.AddComponent<BikeStartState>();

        _stopState = gameObject.AddComponent<BikeStopState>();

        _turnState = gameObject.AddComponent<BikeTurnState>();

        _bikeStateContext.Transition(_stopState);
    }

    public void StartBike() =>
        _bikeStateContext.Transition(_startState);

    public void StopBike() =>
        _bikeStateContext.Transition(_stopState);

    public void RestartBike()
    {
        StopBike();
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    public void Turn(Direction direction)
    {
        CurrentTurnDirection = direction;
        _bikeStateContext.Transition(_turnState);
    }
    
    // ADD: Subscribe to race events when component is enabled
    void OnEnable()
    {
        RaceEventBus.Subscribe(RaceEventType.START, StartBike);
        RaceEventBus.Subscribe(RaceEventType.STOP, StopBike);
        RaceEventBus.Subscribe(RaceEventType.RESTART, RestartBike);
    }
    
    // Your existing StartBike() and StopBike() methods work as is!
    // They already have the right signature for event handlers
    
    
    
    
}