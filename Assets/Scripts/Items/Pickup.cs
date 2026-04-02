using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Powerup powerup;
 
    private void onTriggerEnter(Collider other)
    {
        if (other.GetComponent<BikeController>())
        {
            other.GetComponent<BikeController>().Accept(powerup);
            
            Destroy(gameObject);
        }
    }
}