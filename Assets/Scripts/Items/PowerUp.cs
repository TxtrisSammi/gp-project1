using UnityEngine;

[CreateAssetMenu(fileName = "PowerUp", menuName = "PowerUp")]

public class PowerUp :ScriptableObject, IVisitor
{
    public string powerName;
    public GameObject powerPrefab;
    public string powerUpDescription;

    [Tooltip("Fully Heal Shield")] public bool healShield;

    [Range(0.0f, 50.0f)]
    [Tooltip("Boost turbo settings up to 50 MPH")]
    public float turboBoost;

    [Range(0.0f, 25f)] [Tooltip("Weapon range settings up to 25 units")]
    public int weaponRange;

    public void Visit(BikeShield bikeShield)
    {
        if (healShield)
            bikeShield.health = 100.0f;
    }
    
    public void Visit(BikeWeapon bikeWeapon)
    {
        // Boost range (respect max)
        int range = bikeWeapon.range += weaponRange;
        if (range >= bikeWeapon.maxRange)
            bikeWeapon.range = bikeWeapon.maxRange;
        else 
        {
            bikeWeapon.range = range;
        }
        
        // Boot Strength
        float strength = bikeWeapon.strength += Mathf.Round(bikeWeapon.strength * weaponStrength / 100);

        if (strength >= bikeWeapon.maxStrength)
            bikeWeapon.strength = bikeWeapon.maxStrength;
        else
        {
            bikeWeapon.strength = strength;
        }
        
    }
    
    public void Visit(BikeEngine bikeEngine)
    {
        float boost = bikeEngine.turboBoost += turboBoost;

        // Ensure non-negative
        if (boost < 0.0f)
            bikeEngine.turboBoost = 0.0f;
        
        // Respect max
        if (boost >= bikeEngine.maxTurboBoost)
        {
            bikeEngine.turboBoost = bikeEngine.maxTurboBoost;
        }
    }
}