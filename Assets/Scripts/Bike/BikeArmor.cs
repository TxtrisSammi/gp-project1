using UnityEngine;

public class BikeArmor : MonoBehaviour, IBikeElement
{
    public float armorRating = 0f;
    public float maxArmor = 100f;
    
    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}