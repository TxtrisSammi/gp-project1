using UnityEngine;

public class TestPanel : MonoBehaviour
{
    // Window configuration
    private Rect _windowRect = new Rect(10, 10, 220, 400);
    private bool _isMinimized = false;
    private Vector2 _scrollPosition;

    // Section expansion state
    private bool _stateExpanded = true;
    private bool _eventBusExpanded = true;
    // private bool _commandExpanded = true;  // Uncomment after Lecture 5

    // Cached component references
    private BikeController _bikeController;
    // private Invoker _invoker;  // Uncomment after Lecture 5 (Command Pattern)

    void Start()
    {
        _bikeController = FindFirstObjectByType<BikeController>();
        // _invoker = FindFirstObjectByType<Invoker>();  // Uncomment after Lecture 5
    }

    void OnGUI()
    {
        _windowRect = GUILayout.Window(0, _windowRect,
            DrawWindow, "Test Panel");
    }

    void DrawWindow(int windowID)
    {
        if (GUILayout.Button(_isMinimized ? "+" : "-"))
            _isMinimized = !_isMinimized;

        if (!_isMinimized)
        {
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);
            DrawStatePatternSection();
            DrawEventBusSection();
            // DrawCommandSection();  // Uncomment after Lecture 5
            GUILayout.EndScrollView();
        }

        GUI.DragWindow(); // Makes window draggable
    }


    // Continuing TestPanel class...

    void DrawStatePatternSection()
    {
        if (_bikeController == null) return; // Only show if component exists

        GUI.backgroundColor = Color.cyan;
        _stateExpanded = GUILayout.Toggle(_stateExpanded, "▼ State Pattern", "button");
        GUI.backgroundColor = Color.white;

        if (_stateExpanded)
        {
            GUILayout.BeginVertical("box");
            if (GUILayout.Button("Start Bike")) _bikeController.StartBike();
            if (GUILayout.Button("Stop Bike")) _bikeController.StopBike();
            if (GUILayout.Button("Turn Left")) _bikeController.Turn(Direction.Left);
            if (GUILayout.Button("Turn Right")) _bikeController.Turn(Direction.Right);
            GUILayout.EndVertical();
        }
    }

    void DrawEventBusSection()
    {
        GUI.backgroundColor = Color.yellow;
        _eventBusExpanded = GUILayout.Toggle(_eventBusExpanded, "▼ Event Bus", "button");
        GUI.backgroundColor = Color.white;

        if (_eventBusExpanded)
        {
            GUILayout.BeginVertical("box");
            if (GUILayout.Button("Countdown")) RaceEventBus.Publish(RaceEventType.COUNTDOWN);
            if (GUILayout.Button("Stop")) RaceEventBus.Publish(RaceEventType.STOP);
            if (GUILayout.Button("Restart")) RaceEventBus.Publish(RaceEventType.RESTART);
            if (GUILayout.Button("Finish")) RaceEventBus.Publish(RaceEventType.FINISH);
            if (GUILayout.Button("Pause")) RaceEventBus.Publish(RaceEventType.PAUSE);
            GUILayout.EndVertical();
        }
    }

    // Uncomment after Lecture 5 (Command Pattern) when Invoker class exists:
    /*
    void DrawCommandSection()
    {
        if (_invoker == null) return;

        GUI.backgroundColor = Color.green;
        _commandExpanded = GUILayout.Toggle(_commandExpanded, "▼ Command Pattern", "button");
        GUI.backgroundColor = Color.white;

        if (_commandExpanded)
        {
            GUILayout.BeginVertical("box");
            if (GUILayout.Button("Start Recording")) _invoker.StartRecording();
            if (GUILayout.Button("Stop Recording")) _invoker.StopRecording();
            if (GUILayout.Button("Play Replay")) _invoker.StartReplay();
            GUILayout.EndVertical();
        }
    }
    */
}