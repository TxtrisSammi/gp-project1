using UnityEngine;

public class DynamicDrone : MonoBehaviour
{
    private Drone _drone;
    private BoppingManeuver _bop;
    private WeavingManeuver _weave;
    private BikeController _bike;

    [SerializeField] private float _attackDistance = 10f;

    void Start()
    {
        _drone = GetComponent<Drone>();
        _bop = GetComponent<BoppingManeuver>();
        _weave = GetComponent<WeavingManeuver>();
        _bike = FindFirstObjectByType<BikeController>();
        
        // Start with bopping
        _drone.ApplyStrategy(_bop);
    }
    
    private void Update()
    {
        // Switch to weaving when player gets close
        if (PlayerDistance() < _attackDistance)
        {
            StopAllCoroutines(); // Stop current strategy
            _drone.ApplyStrategy(_weave);
        }
    }
    
    private float PlayerDistance()
    {
        return _drone.transform.position.y - _bike.transform.position.y;
    }
}