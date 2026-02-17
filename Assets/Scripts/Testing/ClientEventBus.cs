using UnityEngine;

public class ClientEventBus : MonoBehaviour
{
    private bool _isButtonEnabled;

    void Start() { _isButtonEnabled = true; }

    void OnEnable()
    {
        RaceEventBus.Subscribe(RaceEventType.START, EnableButton);
        RaceEventBus.Subscribe(RaceEventType.FINISH, EnableButton);
    }

    void OnDisable()
    {
        RaceEventBus.Unsubscribe(RaceEventType.START, EnableButton);
        RaceEventBus.Unsubscribe(RaceEventType.FINISH, EnableButton);
    }

    void EnableButton() { _isButtonEnabled = true; }

    void OnGUI()
    {
        if (GUILayout.Button("Start Countdown"))
        {
            _isButtonEnabled = false;
            RaceEventBus.Publish(RaceEventType.COUNTDOWN);
        }
        if (_isButtonEnabled)
        {
            if (GUILayout.Button("Stop Race"))
                RaceEventBus.Publish(RaceEventType.STOP);
            if (GUILayout.Button("Restart Race"))
                RaceEventBus.Publish(RaceEventType.RESTART);
            if (GUILayout.Button("Finish Race"))
                RaceEventBus.Publish(RaceEventType.FINISH);
            if (GUILayout.Button("Pause"))
                RaceEventBus.Publish(RaceEventType.PAUSE);
        }
    }
}