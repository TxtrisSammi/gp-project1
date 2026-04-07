using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon attachment", menuName = "Weapon/Attachment", order = 1)]

public class WeaponAttachment : ScriptableObject, IWeapon
{
    [Range(0,50)] [Tooltip("Increase Rate of Fire per second")] [SerializeField]
    private float rate;
    
    [Range(0,50)] [Tooltip("Increase Weapon Range")] [SerializeField]
    private float range;

    [Range(0,100)] [Tooltip("Increase Weapon Strength")] [SerializeField]
    private float strength;
    
    [Range(0,-5)] [Tooltip("Decrease Cooldown Duration")] [SerializeField]
    private float cooldown;
    
    public string attachmentName;
    public GameObject attacmentPrefab;
    public string attacmentDescription;
    
    // IWeapon Implement
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