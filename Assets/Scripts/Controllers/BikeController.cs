using UnityEngine;
using System.Collections.Generic;

public class BikeController : MonoBehaviour, IBikeElement
{
    // Configuration
    public float maxSpeed = 2.0f;
    public float turnDistance = 2.0f;
    public Transform startPOS;
    
//  private List<IBikeElement> _bikeElements = new List<IBikeElement>();
    private readonly List<IBikeElement> _bikeElements = new();

    // Current state data
    public float CurrentSpeed { get; set; }
    public Direction CurrentTurnDirection { get; private set; }

    // State References
    private IBikeState _startState, _stopState, _turnState, _crashState;
    private BikeStateContext _bikeStateContext;
    private Animator _animator;
    private Invoker _invoker;
    
    // Observer Events (publishers)
    public event System.Action<float> OnDamage;
    public event System.Action OnTurboStart;
    public event System.Action OnTurboEnd;
    public event System.Action OnHealthCritical;
    
    public int damageCount { get; private set; }
    public int turboCount { get; private set; }
    public int criticalCount { get; private set; }
    
    // Health State
    public float health = 100.0f;
    public float maxHealth = 100.0f;
    public bool _criticalTriggered = false;
    public bool isTurboActive;


    void Start()
    {
        _bikeStateContext = new BikeStateContext(this);

        _startState = gameObject.AddComponent<BikeStartState>();
        _stopState = gameObject.AddComponent<BikeStopState>();
        _turnState = gameObject.AddComponent<BikeTurnState>();
        _crashState = gameObject.AddComponent<BikeCrashState>();
        
        _bikeElements.Add(gameObject.AddComponent<BikeShield>());
//        _bikeElements.Add(gameObject.AddComponent<BikeWeapon>()); // Commented out so I can add Weapon manually due to null exception
        _bikeElements.Add(gameObject.AddComponent<BikeEngine>());


        _animator = GetComponent<Animator>();
        _bikeStateContext.Transition(_stopState);
        _invoker = GetComponent<Invoker>();
    }

    // ADD: Subscribe to race events when component is enabled
    void OnEnable()
    {
        RaceEventBus.Subscribe(RaceEventType.START, StartBike);
        RaceEventBus.Subscribe(RaceEventType.STOP, StopBike);
        RaceEventBus.Subscribe(RaceEventType.RESTART, RestartBike);
    }
    
    void OnDisable()
    {
        RaceEventBus.Unsubscribe(RaceEventType.START, StartBike);
        RaceEventBus.Unsubscribe(RaceEventType.STOP, StopBike);
        RaceEventBus.Unsubscribe(RaceEventType.RESTART, RestartBike);
    }

    public void StartBike() 
    {
        _bikeStateContext.Transition(_startState);
        _animator.SetTrigger("StartMoving");
    }
       
    public void StopBike()
    {
        _bikeStateContext.Transition(_stopState);
        _animator.SetTrigger("StopMoving");
    }

    public void RestartBike()
    {
        StopBike();
        transform.position = startPOS.transform.position;
        transform.rotation = startPOS.transform.rotation;
    }
    
    public void CrashBike()
    {
        _bikeStateContext.Transition(_crashState);
        _animator.SetTrigger("Crash");
    }

    public void Turn(Direction direction)
    {
        // State Pattern Implementation
        CurrentTurnDirection = direction;
        _bikeStateContext.Transition(_turnState);



        // from videos
        // if (direction == Direction.Left)
        // {
            // transform.Rotate(Vector3.down * turnDistance);
            // Debug.Log("[BikeController] Turn Left");
        // }
        // else if (direction == Direction.Right)
        // {
            // transform.Rotate(Vector3.up * turnDistance);
            // Debug.Log("[BikeController] Turn Right");
        // }
   }
    
    public void TakeDamage(float amount)
    {
        health -= amount;
        health = Mathf.Max(health, 0);
        damageCount ++;
        OnDamage?.Invoke(amount);

        if (health / maxHealth <= 0.25f && !_criticalTriggered)
        {
            _criticalTriggered = true;
            criticalCount ++;
            OnHealthCritical!.Invoke();
        }
    }
    
    public void ActivateTurbo()
    {
        if (!isTurboActive)
        {
            isTurboActive = true;
            turboCount ++;
            OnTurboStart?.Invoke();
        }
    }
    
    public void DeactivateTurbo()
    {
        if (isTurboActive)
        {
            isTurboActive = false;
            OnTurboEnd?.Invoke();
        }
    }
    
    public void Accept(IVisitor visitor)
    {
        // Forward visitor to all bike elements
        foreach(IBikeElement element in _bikeElements)
        {
            element.Accept(visitor);
        }
    }
}