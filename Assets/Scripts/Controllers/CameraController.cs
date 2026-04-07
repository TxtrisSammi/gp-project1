using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public float shakeIntensity = 0.3f;
    public float shakeDuration = 0.5f;
    private Vector3 _originalPosition;
    private Coroutine _shakeCoroutine;
    private BikeController _bikeController;

    void Start()
    {
        _bikeController = FindFirstObjectByType<BikeController>();
        _originalPosition = transform.position;
        _bikeController.OnDamage += HandleDamage;
        _bikeController.OnHealthCritical += HandleHealthCritical;
    }

    void OnDestroy() 
    {
        if (_bikeController != null)
        {
            _bikeController.OnDamage -= HandleDamage;
            _bikeController.OnHealthCritical -= HandleHealthCritical;
        }
    }

    private void HandleDamage(float amount)
    {
        // Scale Intensity by damage relative to 25
        float intensity = shakeIntensity * (amount / 25f);
        
        // stop any existing shake before starting a new one
        if (_shakeCoroutine != null)
            StopCoroutine(_shakeCoroutine);
        _shakeCoroutine = StartCoroutine(ShakeCamera(intensity));
    }

    private IEnumerator ShakeCamera(float intensity)
    {
        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            // Decay intensity over time
            float currentIntensity = Mathf.Lerp(intensity, 0f, elapsed / shakeDuration);

            float x = Random.Range(-1f, 1f) * currentIntensity;
            float y = Random.Range(-1f, 1f) * currentIntensity;
            transform.position = _originalPosition + new Vector3(x, y, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = _originalPosition;
        _shakeCoroutine = null;
    }

    private void HandleHealthCritical()
    {
        Debug.Log("[Camera] Red Tint Activated");
        Camera.main.backgroundColor = Color.red;
    }
}
