using UnityEngine;
using System.Collections;

public class CirclingManeuver : MonoBehaviour, IManeuverBehaviour
{
    public void Maneuver(Drone drone)
    {
        StartCoroutine(Circle(drone));
    }
    
    IEnumerator Circle(Drone drone)
    {
        float radius = 3f;
        float angle = 0f;
        Vector3 center = drone.transform.position;

        while (true)
        {
            angle += drone.speed * Time.deltaTime;
            float x = center.x + Mathf.Cos(angle) * radius;
            float z =  center.z + Mathf.Sin(angle) * radius;
            drone.transform.position = new Vector3(x, center.y, z);
            yield return null;
        }
    }
}