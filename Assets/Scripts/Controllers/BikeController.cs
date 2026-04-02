using UnityEngine;
using System.Collections.Generic;

public class BikeController : MonoBehaviour, IBikeElement
{
    // Configuration
    public float maxSpeed = 2.0f;
    public float turnDistance = 2.0f;
    public Transform StartPOS;
    
    private List _bikeElements = new List();

    // Current state data
    public float CurrentSpeed { get; set; }
    public Direction CurrentTurnDirection { get; private set; }

    // State References
    private IBikeState _startState, _stopState, _turnState;
    private BikeStateContext _bikeStateContext;
    private Animator _animator;
    private Invoker _invoker;

    void Start()
    {
        _bikeStateContext = new BikeStateContext(this);

        _startState = gameObject.AddComponent<BikeStartState>();
        _stopState = gameObject.AddComponent<BikeStopState>();
        _turnState = gameObject.AddComponent<BikeTurnState>();

        _animator = GetComponent<Animator>();
        _bikeStateContext.Transition(_stopState);
        _invoker = GetComponent<Invoker>();
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
// from videos
        if (direction == Direction.Left)
        {
            transform.Translate(Vector3.left * turnDistance);
            Debug.Log("[BikeController] Turn Left");
        }
        else if (direction == Direction.Right)
        {
            transform.Translate(Vector3.right * turnDistance);
            Debug.Log("[BikeController] Turn Right");
        }



        // from slides 
    //    CurrentTurnDirection = direction;
    //   _bikeStateContext.Transition(_turnState);
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
