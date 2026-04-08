using UnityEngine;

public class SegmentMarker : MonoBehaviour
{
    public TrackController trackController;

    private void OnTriggerExit(Collider other)
    {
        // Did bike pass through this marker? 
        if (other.GetComponent<BikeController>())
        {
            // Load 1 new segment ahead
            trackController.LoadSegment(1);

            // Destroy this entire segment
            Destroy(transform.parent.gameObject);
            
            // DEBUG stuff
            Debug.Log("Segment destroyed, loading next...");
        }

    }
}