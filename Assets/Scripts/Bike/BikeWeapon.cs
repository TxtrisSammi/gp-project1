using UnityEngine;
using System.Collections;

public class BikeWeapon : MonoBehaviour, IBikeElement
{
    public WeaponConfig weaponConfig;
    public WeaponAttachment weaponAttachment;
    public WeaponAttachment secondaryAttachment;

    private bool _isFiring;
    private IWeapon _weapon;
    private bool _isDecorated;

    [Header("Range")] public int range = 5;
    public int maxRange = 25;
   
    [Header("Strength")] public float strength = 25.0f;
    public float maxStrength = 50.0f;

    public void Fire() { Debug.Log("Weapon fired!"); }
    
    void Start()
    {
        // Initialize Base Weapon
        _weapon = new Weapon(weaponConfig);
        
    }
    
    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this); // Double Dispatch
    }
    
    public void Decorate()
    {
        // One attachment
        if (weaponAttachment && !secondaryAttachment)
        {
            _weapon = new WeaponDecorator(_weapon, weaponAttachment);
        }
        
        if (weaponAttachment && secondaryAttachment)
        {
            _weapon = new WeaponDecorator(new WeaponDecorator(_weapon, weaponAttachment), secondaryAttachment);
        }
        
//        _isDecorated = !_isDecorated;
    }
    
    public void Reset()
    {
        // Remove all decorators - Return to base weapon
        _weapon = new Weapon(weaponConfig);
//        _isDecorated = !_isDecorated; // should make to always set false, could lead to issues
    }
    
    public void ToggleFire()
    {
        _isFiring = !_isFiring;
        if (_isFiring)
            StartCoroutine(FireWeapon());
    }
    
    IEnumerator FireWeapon()
    {
        float firingRate = 1.0f / _weapon.Rate;
        while (_isFiring)
        {
            yield return new WaitForSeconds(firingRate);
            Debug.Log("Fire! Strength: " + _weapon.Strength);
        }
    }
    
    void OnGUI()
    {
        GUI.color = Color.green;

        GUI.Label(new Rect(5, 50, 150, 100), "Range: " + _weapon.Range);

        GUI.Label(new Rect(5, 70, 150, 100), "Strength: " + _weapon.Strength);

        GUI.Label(new Rect(5, 90, 150, 100), "Cooldown: " + _weapon.Cooldown);

        GUI.Label(new Rect(5, 110, 150, 100), "Firing Rate: " + _weapon.Rate);

        GUI.Label(new Rect(5, 130, 150, 100), "Weapon Firing: " + _isFiring);

        if (weaponAttachment && _isDecorated)
            GUI.Label(new Rect(5, 150, 150, 100), "Main: " + weaponAttachment.name);

        if (secondaryAttachment && _isDecorated)
            GUI.Label(new Rect(5, 170, 150, 100), "Secondary: " + secondaryAttachment.name);
    }
}