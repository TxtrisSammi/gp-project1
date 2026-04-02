using UnityEngine;

public class BikeEngine : MonoBehaviour, IBikeElement
{
    public float turboBoost = 25.0f; // mph
    public float maxTurboBoost = 200.0f;
    private bool _isTurboOn;
    private float _defaultSpeed = 300.0f;
    
    public float currentSpeed
    {
        get 
        {
            if (_isTurboOn) 
                return _defaultSpeed * turboBoost;
            return _defaultSpeed;
        }
    }

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this); // Double Dispatch
    }
}