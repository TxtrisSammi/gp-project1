using UnityEngine;

public class BikeWeapon : MonoBehaviour, IBikeElement
{
   [Header("Range")] public int range = 5;
   public int maxRange = 25;
   
   [Header("Strength")] public float strength = 25.0f;
   public float maxStrength = 50.0f;

   public void Fire() { Debug.Log("Weapon fired!"); }
    
    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this); // Double Dispatch
    }
}