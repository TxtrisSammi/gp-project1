using UnityEngine;

public class AudioController : MonoBehaviour
{
    private BikeController _bikeController;

    void Start()
    {
        _bikeController = FindFirstObjectByType<BikeController>();

        // Subscribe to ALL bike events
        _bikeController.OnDamage += HandleDamage;
        _bikeController.OnTurboStart += HandleTurboStart;
        _bikeController.OnTurboEnd += HandleTurboEnd;
        _bikeController.OnHealthCritical += HandleHealthCritical;
    }

    void OnDestroy()
    {
        if (_bikeController != null)
        {
            _bikeController.OnDamage -= HandleDamage;
            _bikeController.OnTurboStart -= HandleTurboStart;
            _bikeController.OnTurboEnd -= HandleTurboEnd;
            _bikeController.OnHealthCritical -= HandleHealthCritical;
        }
    }

    void HandleDamage(float amount)
    {
        Debug.Log("[Audio] Playing damage sound");
        // AudioSource.PlayOneShot(damageClip);
    }

    void HandleTurboStart() =>
        Debug.Log("[Audio] Turbo sound ON");

    void HandleTurboEnd() =>
        Debug.Log("[Audio] Turbo sound OFF");
    void HandleHealthCritical()
    {
        Debug.Log("[Audio] Warning Alarm Playing");
        // AudioSource.PlayOneShot(warningClip);
    }
}