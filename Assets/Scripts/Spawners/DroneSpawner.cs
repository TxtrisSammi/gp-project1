using UnityEngine;
using UnityEngine.Pool;

public class DroneSpawner : MonoBehaviour
{
    public static DroneSpawner Instance { get; private set; }
    
    public Drone dronePrefab;

    public int defaultCapacity = 10;
    public int maxPoolSize = 50;
    public bool collectionCheck = true;
    
    private ObjectPool<Drone> _pool;

    void Awake()
    {
        Instance = this;

        _pool = new ObjectPool<Drone>(
            createFunc: CreateDrone,
            actionOnGet: OnGetDrone, 
            actionOnRelease: OnReleaseDrone,
            actionOnDestroy: OnDestroyDrone,
            collectionCheck: collectionCheck,
            defaultCapacity: defaultCapacity,
            maxSize: maxPoolSize
        );
    }

    private Drone CreateDrone()
    {
        Drone drone = Instantiate(dronePrefab);
        drone.gameObject.SetActive(false);
        Debug.Log("[Pool] Created new drone");
        return drone;
    }
    
    private void OnGetDrone(Drone drone)
    {
        drone.OnSpawn();
        Debug.Log("[Pool] Drone retrived from Pool");
    }
    
    private void OnReleaseDrone(Drone drone)
    {
        drone.OnDespawn();
        Debug.Log("[Pool] Drone released");
    }
    
    private void OnDestroyDrone(Drone drone)
    {
       Destroy(drone.gameObject);
       Debug.Log("[Pool] Drone destroyed");
    }
    
    public Drone SpawnDrone(Vector3 position)
    {
        Drone drone = _pool.Get();
        drone.transform.position = position;
        return drone;
    }
    
    public void ReleaseDrone(Drone drone)
    {
        _pool.Release(drone);
    }
    
    public int GetActiveCount()
    {
        return _pool.CountActive;
    }
    
    public int GetInactiveCount()
    {
        return _pool.CountInactive;
    }
    
    void OnDestroy()
    {
        _pool.Clear();
    }
}