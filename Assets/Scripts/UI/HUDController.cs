using UnityEngine;

public class HUDController : MonoBehaviour
{
    private BikeController _bikeController;
    void OnEnable()
    {
        // Subscribe to events that affect UI FROM EVENT BUS
        RaceEventBus.Subscribe(RaceEventType.START, DisplayStartMessage);
        RaceEventBus.Subscribe(RaceEventType.STOP, DisplayStopMessage);
        RaceEventBus.Subscribe(RaceEventType.FINISH, DisplayFinishMessage);
        RaceEventBus.Subscribe(RaceEventType.PAUSE, DisplayPauseMessage);
        
        // OBSERVER PATTERN
        _bikeController = FindFirstObjectByType<BikeController>();
        _bikeController.OnDamage += HandleDamage;
        _bikeController.OnTurboStart += HandleTurboStart;
        _bikeController.OnHealthCritical += HandleHealthCritical;
    }

    void OnDisable()
    {
        // Clean up subscriptions EVENT BUS
        RaceEventBus.Unsubscribe(RaceEventType.START, DisplayStartMessage);
        RaceEventBus.Unsubscribe(RaceEventType.STOP, DisplayStopMessage);
        RaceEventBus.Unsubscribe(RaceEventType.FINISH, DisplayFinishMessage);
        RaceEventBus.Unsubscribe(RaceEventType.PAUSE, DisplayPauseMessage);
        
        // OBSERVER PATTERN
        if (_bikeController != null)
        {
            _bikeController.OnDamage -= HandleDamage;
            _bikeController.OnTurboStart -= HandleTurboStart;
        }
    }
    
    void OnGUI()
    {
        if (_bikeController == null) return;

        // position in top-right corner
        float x = Screen.width - 220;
        float y = 10;
        float w = 210;
        float h = 160;

        // Background box
        GUI.Box(new Rect(x, y, w, h), "Observer Status");

        // Health Display
        string healthColor = _bikeController.health / _bikeController.maxHealth < 0.25f ? "red" : "white";
        GUI.Label(new Rect(x + 10, y + 25, w - 20, 20),
            $"<color={healthColor}>Health: " +
            $"{_bikeController.health:F0} / " +
            $"{_bikeController.maxHealth:F0}</color>");
        
        // Turbo Status
        string turboStatus = _bikeController.isTurboActive ? "<color=cyan>TURBO: ON</color>" : "TURBO: OFF";
        GUI.Label(new Rect(x + 10, y + 50, w - 20, 20), turboStatus);

        // Event Counters
        GUI.Label(new Rect(x+10, y+80, w-20, 20),
            "--- Event Counts ---");
        GUI.Label(new Rect(x+10, y+100, w-20, 20),
            $"Damage: {_bikeController.damageCount}");
        GUI.Label(new Rect(x+10, y+120, w-20, 20),
            $"Turbo: {_bikeController.turboCount}");
        GUI.Label(new Rect(x+10, y+140, w-20, 20),
            $"Critical: {_bikeController.criticalCount}"); 
    }

    void HandleHealthCritical()
    {
        Debug.Log("[HUD] CRITICAL! Flashing Red!");
        // Flash HUD red - visual warning
    }

    void HandleDamage(float amount) =>
        Debug.Log($"[HUD] Health Updated");
    
    void HandleTurboStart () =>
        Debug.Log($"[HUD] Turbo Activated!");

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