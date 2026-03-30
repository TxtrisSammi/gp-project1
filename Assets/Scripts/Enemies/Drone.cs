using UnityEngine;

public class Drone : MonoBehaviour
{
    public float speed = 5f;
    public float maxHeight = 10f;
    public float wobbleAmount = 10f;
    
    public void OnSpawn()
    {
        gameObject.SetActive(true);
        Debug.Log("[Drone] Spawned and activated");
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
        
        if (transform.position.y > maxHeight)
        {
            DroneSpawner.Instance.ReleaseDrone(this);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

}
