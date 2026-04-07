using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Config",
    menuName = "Weapon/Config", order = 1)]

public class WeaponConfig : ScriptableObject, IWeapon
{
    [Range(0,60)] [Tooltip("Rate of Fire per second")] [SerializeField]
    private float rate;
    
    [Range(0,50)] [Tooltip("Weapon Range")] [SerializeField]
    private float range;

    [Range(0,100)] [Tooltip("Weapon Strength")] [SerializeField]
    private float strength;
    
    [Range(0, 5)] [Tooltip("Cooldown Duration")] [SerializeField]
    private float cooldown;

    public string WeaponName;
    public GameObject weaponPrefab;
    public string weaponDescription;

    // IWeapon implementation
    public float Rate
    {
        get{ return rate; }
    }

    public float Range
    {
        get{ return range; }
    }

    public float Strength
    {
        get{ return strength; }
    }

    public float Cooldown
    {
        get{ return cooldown; }
    }


}                        