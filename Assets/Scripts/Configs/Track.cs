using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Track", menuName = "Track")]

public class Track : ScriptableObject
{
    public string trackName;
    public float segmentLength = 40f;
    public List<GameObject> segments;
}