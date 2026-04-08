using UnityEngine;
using System.Collections.Generic;

public class TrackController : MonoBehaviour
{
    public Track track;
    public BikeController bikeController;

    private Stack<GameObject> _segStack;
    private Transform _segParent;
    private float _zPos;
    private int _segCount;

    void Start()
    {
        _segParent = GameObject.Find("Track").transform;
        _segStack = new Stack<GameObject>();
        
        // Initialize stack with segments in REVERSE order.
        for (int i = track.segments.Count - 1; i >= 0; i--)
        {
            _segStack.Push(track.segments[i]);
            
        }
        
        LoadSegment(3);
    }
    
    public void LoadSegment(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (_segStack.Count > 0)
            {
                // Get next seg from stack
                GameObject segment = Instantiate(_segStack.Pop(), Vector3.zero, Quaternion.identity);
            
                // Set as child of track parent
                segment.transform.SetParent(_segParent);

                // Position segment ahead of player
                segment.transform.localPosition = new Vector3(0, 0, _zPos);

                // Move position for next segment
                _zPos += track.segmentLength;
                _segCount++;
                
                // DEBUG STUFF
                Debug.Log($"Loaded segment {_segCount}, Stack count: {_segStack.Count}");
            }

        }
    }
}