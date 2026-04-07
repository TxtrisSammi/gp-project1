using UnityEngine;

public class Drone : MonoBehaviour
{
    // Strategy Pattern (Parameters for laser)
    private RaycastHit _hit;
    private Vector3 _rayDirection;
    private float _rayAngle = -45.0f;
    private float _rayDistance = 15.0f;
    
    // Movement parameters
    [Header("Drone Movement")]
    public float speed = 5f;
    public float maxHeight = 10f;
    public float wobbleAmount = 10f;
    public float weavingDistance = 1.5f;
    public float fallbackDistance = 20f;
    
    public void OnSpawn()
    {
        gameObject.SetActive(true);
        
        transform.rotation = Quaternion.identity;
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        Debug.Log("[Drone] Spawned and activated");
    }
    
    void Start()
    {
        // Setup laser direction
        _rayDirection = transform.TransformDirection(Vector3.back) * _rayDistance;
        _rayDirection = Quaternion.Euler(_rayAngle, 0f, 0f) * _rayDirection;
    }
    
    public void ApplyStrategy(IManeuverBehaviour strategy)
    {
        strategy.Maneuver(this);
    }

    public void OnDespawn()
    {
        gameObject.SetActive(false);
        Debug.Log("[Drone] Despawned and deactivated");

    }

    // Update is called once per frame
    void Update()
    {
        float wobble = Mathf.Sin(Time.time * wobbleAmount);
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        transform.Translate(Vector3.right * wobble * Time.deltaTime);
        
        Debug.DrawRay(transform.position, _rayDirection, Color.blue);
        
        if (transform.position.y > maxHeight)
        {
            DroneSpawner.Instance.ReleaseDrone(this);
        }
        
        
    }
}
