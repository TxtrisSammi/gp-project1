using UnityEngine;
using System.Collections;

public class CountdownTimer : MonoBehaviour
{
    private float _currentTime;
    private float duration = 3.0f;

    void OnEnable()
    {
        RaceEventBus.Subscribe(RaceEventType.COUNTDOWN, StartTimer);
    }

    void OnDisable()
    {
        RaceEventBus.Unsubscribe(RaceEventType.COUNTDOWN, StartTimer);
    }

    void StartTimer()
    {
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        _currentTime = duration;
        while (_currentTime > 0)
        {
            Debug.Log("Countdown: " + Mathf.Ceil(_currentTime));
            yield return new WaitForSeconds(1.0f);
            _currentTime--;
        }
        Debug.Log("GO!");
        RaceEventBus.Publish(RaceEventType.START);
    }
}
