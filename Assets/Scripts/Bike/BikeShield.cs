using UnityEngine;

public class BikeShield : MonoBehaviour, IBikeElement
{
    public float health = 50.0f;

    public float Damange (float damage)
    {
        health -= damage;
        return health;
    }
    
    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this); // Double Dispatch
    }
}