using UnityEngine;

public class HUDController : MonoBehaviour
{
    void OnEnable()
    {
        // Subscribe to events that affect UI
        RaceEventBus.Subscribe(RaceEventType.START, DisplayStartMessage);
        RaceEventBus.Subscribe(RaceEventType.STOP, DisplayStopMessage);
        RaceEventBus.Subscribe(RaceEventType.FINISH, DisplayFinishMessage);
        RaceEventBus.Subscribe(RaceEventType.PAUSE, DisplayPauseMessage);
    }

    void OnDisable()
    {
        // Clean up subscriptions
        RaceEventBus.Unsubscribe(RaceEventType.START, DisplayStartMessage);
        RaceEventBus.Unsubscribe(RaceEventType.STOP, DisplayStopMessage);
        RaceEventBus.Unsubscribe(RaceEventType.FINISH, DisplayFinishMessage);
        RaceEventBus.Unsubscribe(RaceEventType.PAUSE, DisplayPauseMessage);
    }

    void DisplayStartMessage()
    {
        Debug.Log("[HUD] GO! Race has started!");
        // In real implementation: Update UI text, show animation
    }

    void DisplayStopMessage()
    {
        Debug.Log("[HUD] Race stopped");
    }

    void DisplayFinishMessage()
    {
        Debug.Log("[HUD] FINISH! You completed the race!");
    }

    void DisplayPauseMessage()
    {
        Debug.Log("[HUD] PAUSED");
    }
}