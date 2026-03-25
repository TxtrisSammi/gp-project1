using UnityEngine;

public class TestPanel : MonoBehaviour
{
    // Window configuration
    private Rect _windowRect = new Rect(10, 10, 220, 400);
    private bool _isMinimized = false;
    private Vector2 _scrollPosition;

    private bool _showKeymap = false;
    private Rect _keymapRect = new Rect(250, 10, 200, 250);


    // Section expansion state
    private bool _stateExpanded = true;
    private bool _eventBusExpanded = true;
    private bool _commandExpanded = true;  // Uncomment after Lecture 5

    // Cached component references
    private BikeController _bikeController;
    private Invoker _invoker;  // Uncomment after Lecture 5 (Command Pattern)
    private ICommand _turnLeft, _turnRight;

    void Start()
    {
        _bikeController =  FindFirstObjectByType<BikeController>();
        _invoker = FindFirstObjectByType<Invoker>();  // Uncomment after Lecture 5
        
        if (_bikeController != null) 
        {
            _turnLeft = new TurnLeft(_bikeController);
            _turnRight = new TurnRight(_bikeController);
        }
    }

       void Update()
    {
        // Event Bus keyboard shortcuts
        if (Input.GetKeyDown(KeyCode.C))
            RaceEventBus.Publish(RaceEventType.COUNTDOWN);
        if (Input.GetKeyDown(KeyCode.S))
            RaceEventBus.Publish(RaceEventType.STOP);
        if (Input.GetKeyDown(KeyCode.R))
            RaceEventBus.Publish(RaceEventType.RESTART);
        if (Input.GetKeyDown(KeyCode.F))
            RaceEventBus.Publish(RaceEventType.FINISH);
        if (Input.GetKeyDown(KeyCode.P))
            RaceEventBus.Publish(RaceEventType.PAUSE);
        if (Input.GetKeyDown(KeyCode.Q))
            RaceEventBus.Publish(RaceEventType.QUIT);

        // Toggle keymap with K key
        if (Input.GetKeyDown(KeyCode.K))
            _showKeymap = !_showKeymap;
        // Command Pattern Shortcuts
       if (_invoker != null)
        {
            if (Input.GetKeyDown(KeyCode.A))
                _invoker.ExecuteCommand(_turnLeft);
            if (Input.GetKeyDown(KeyCode.D))
                _invoker.ExecuteCommand(_turnRight);
            if (Input.GetKeyDown(KeyCode.Alpha1))
                _invoker.StartRecording();
            if (Input.GetKeyDown(KeyCode.Alpha2))
                _invoker.StopRecording();
            if (Input.GetKeyDown(KeyCode.Alpha3))
                _invoker.StartReplay();
        } 
    }


    void OnGUI()
    {
        _windowRect = GUILayout.Window(0, _windowRect,
            DrawWindow, "Test Panel");

        // Show keymap window if enabled
        if (_showKeymap)
            _keymapRect = GUILayout.Window(1, _keymapRect,
                DrawKeymapWindow, "Keyboard Shortcuts");
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

        // Add to DrawWindow() after minimize button:
        if (GUILayout.Button(_showKeymap ? "Hide Keymap (K)" : "Show Keymap (K)"))
            _showKeymap = !_showKeymap;

        GUI.DragWindow(); // Makes window draggable
        DrawCommandSection();
    }


    // Continuing TestPanel class...

    void DrawStatePatternSection()
    {
        if (_bikeController != null) return; // Only show if component exists

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

    // Uncomment after Lecture 6 (Command Pattern) when Invoker class exists:
    
    void DrawCommandSection()
    {
        if (_invoker != null) return;

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
    

    void DrawKeymapWindow(int windowID)
    {
        GUILayout.Label("--- Event Bus ---");
        GUILayout.Label("C = Countdown");
        GUILayout.Label("S = Stop");
        GUILayout.Label("R = Restart");
        GUILayout.Label("F = Finish");
        GUILayout.Label("P = Pause");
        GUILayout.Label("Q = Quit");
        GUILayout.Space(10);
        GUILayout.Label("--- Command Pattern");
        GUILayout.Label("A = Turn Left");
        GUILayout.Label("D = Turn Right");
        GUILayout.Label("1 = Start Recording");
        GUILayout.Label("2 = Stop Recording");
        GUILayout.Label("3 = Play Replay");
        GUILayout.Space(10);
        GUILayout.Label("--- General ---");
        GUILayout.Label("K = Toggle this keymap");
        GUILayout.Space(10);
        if (GUILayout.Button("Close"))
            _showKeymap = false;
        GUI.DragWindow();
    }
}