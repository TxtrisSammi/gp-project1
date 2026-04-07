using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestPanel : MonoBehaviour
{
    // Window configuration
    private Rect _windowRect = new Rect(10, 10, 220, 400);
    private bool _isMinimized = false;
    private Vector2 _scrollPosition;
    private bool _showKeymap = false;
    private Rect _keymapRect = new Rect(250, 10, 200, 250);

    // Window Enhancments
    private bool _showTestPanel = true;
    private bool _isResizing;
    private Vector2 _minSize = new Vector2(200, 100);
    private Vector2 _maxSize = new Vector2(400, 800);

    // Section expansion state
    private bool _stateExpanded = false;
    private bool _eventBusExpanded = false;
    private bool _commandExpanded = false;
    private bool _poolExpanded = false;
    private bool _observerExpanded = false;
    private bool _visitorExpanded = false;
    private bool _strategyExpanded = true;

    // Cached component references
    private GameObject _drone;
    public GameObject dronePrefab;

    private BikeController _bikeController;
    private Invoker _invoker;  // Uncomment after Lecture 5 (Command Pattern)
    private DroneSpawner _spawner;

    private ICommand _turnLeft, _turnRight;
    private List<IManeuverBehaviour> _components = new List<IManeuverBehaviour>();

    private bool _autoSpawning = false;
    private float _spawnInterval = 0.5f;
    private float _spawnRange = 5f;
    private float _nextSpawnTime;

    // Visitor Pattern
    public Powerup shieldPowerup;
    public Powerup enginePowerup;
    public Powerup WeaponPowerup;

    void Start()
    {
        _bikeController = FindFirstObjectByType<BikeController>();
        _invoker = FindFirstObjectByType<Invoker>();  // Uncomment after Lecture 5
        _spawner = FindFirstObjectByType<DroneSpawner>();

        if (_bikeController != null)
        {
            _turnLeft = new TurnLeft(_bikeController);
            _turnRight = new TurnRight(_bikeController);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
            _showTestPanel = !_showTestPanel;

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

        if (_spawner != null)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                Vector3 pos = new Vector3(Random.Range(-_spawnRange, _spawnRange), 0f, Random.Range(-_spawnRange, _spawnRange));
                _spawner.SpawnDrone(pos);
            }

            if (Input.GetKeyDown(KeyCode.T))
                _autoSpawning = !_autoSpawning;

            if (_autoSpawning && Time.time > _nextSpawnTime)
            {
                Vector3 pos = new Vector3(Random.Range(-_spawnRange, _spawnRange), 0f, Random.Range(-_spawnRange, _spawnRange));
                _spawner.SpawnDrone(pos);
                _nextSpawnTime = Time.time + _spawnInterval;
            }
        }

        if (_bikeController != null)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                _bikeController.Accept(shieldPowerup);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                _bikeController.Accept(enginePowerup);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                _bikeController.Accept(WeaponPowerup);
            }
        }

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

        if (_bikeController != null)
        {
            if (Input.GetKeyDown(KeyCode.H))
                _bikeController.TakeDamage(25f);
            if (Input.GetKeyDown(KeyCode.B))
            {
                if (_bikeController.isTurboActive)
                    _bikeController.DeactivateTurbo();
                else
                    _bikeController.ActivateTurbo();
            }
        }
        
        // Strategy Pattern
        if (Input.GetKeyDown(KeyCode.G))
            SpawnDrone();

        // Toggle keymap with K key
        if (Input.GetKeyDown(KeyCode.K))
            _showKeymap = !_showKeymap;
    }

    void OnGUI()
    {
        _windowRect = GUILayout.Window(0, _windowRect, DrawWindow, "Test Panel");

        if (_showKeymap)
            _keymapRect = GUILayout.Window(1, _keymapRect, DrawKeymapWindow, "Keyboard Shortcuts");
        if (!_showTestPanel) return;

        _windowRect = GUILayout.Window(0, _windowRect, DrawWindow, "Test Panel");

        // Show keymap window if enabled
        if (_showKeymap)
            _keymapRect = GUILayout.Window(1, _keymapRect,
                DrawKeymapWindow, "Keyboard Shortcuts");
    }

    private void SpawnDrone()
    {
        // From Slides
        _drone = Instantiate(dronePrefab);
        // From Video
//        _drone = GameObject.CreatePrimitive(PrimitiveType.Cube);
//        _drone.AddComponent<Drone>();

        // In both
        _drone.transform.position = Random.insideUnitSphere * 10;

        ApplyRandomStrategies();
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

        GUILayout.Label("--- Object Pool ---");
        GUILayout.Label("G = Spawn Drone");
        GUILayout.Label("T = Toggle Auto-Spawn");
        GUILayout.Space(10);

        GUILayout.Label("--- ObserverPattern ---");
        GUILayout.Label("D = Take Damage");
        GUILayout.Label("B = Toggle Turbo");
        GUILayout.Space(10);

        GUILayout.Label("--- Visitor Pattern ---");
        GUILayout.Label("V = Shield Powerup");
        GUILayout.Label("E = Engine Powerup");
        GUILayout.Label("W = Weapon Powerup");

        GUILayout.Label("--- General ---");
        GUILayout.Label("K = Toggle this keymap");
        GUILayout.Space(10);
        if (GUILayout.Button("Close"))
            _showKeymap = false;
        GUI.DragWindow();
    }

    void DrawWindow(int windowID)
    {
        if (GUILayout.Button(_showKeymap ? "Hide Keymap (K)" : "Show Keymap (K)"))
            _showKeymap = !_showKeymap;
        if (GUILayout.Button(_isMinimized ? "+" : "-"))
            _isMinimized = !_isMinimized;

        GUILayout.BeginHorizontal();
        if (GUILayout.Button(_isMinimized ? "+" : "-", GUILayout.Width(25f)))
            _isMinimized = !_isMinimized;

        if (GUILayout.Button("X", GUILayout.Width(25)))
            _showTestPanel = false;
        GUILayout.EndHorizontal();

        if (!_isMinimized)
        {
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);
            DrawStatePatternSection();
            DrawEventBusSection();
            DrawCommandSection();  // Uncomment after Lecture 5
            DrawPoolSection();
            DrawObserverSection();
            DrawVisitorSection();
            DrawStrategySection();
            GUILayout.EndScrollView();
        }


        // Draw Section Methods
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

        void DrawCommandSection()
        {
            if (_invoker == null) return;
            GUI.backgroundColor = Color.green;
            _commandExpanded = GUILayout.Toggle(_commandExpanded, "▼ Command Pattern", "button");
            GUI.backgroundColor = Color.white;

            if (_commandExpanded)
            {
                GUILayout.BeginVertical("box");
                if (GUILayout.Button("Turn Left"))
                    _invoker.ExecuteCommand((_turnLeft));
                if (GUILayout.Button("Turn Right"))
                    _invoker.ExecuteCommand((_turnRight));
                if (GUILayout.Button("Start Recording"))
                    _invoker.StartRecording();
                if (GUILayout.Button("Stop Recording"))
                    _invoker.StopRecording();
                if (GUILayout.Button("Play Replay"))
                    _invoker.StartReplay();
                GUILayout.EndVertical();
            }
        }

        void DrawPoolSection()
        {
            if (_spawner == null) return;
            GUI.backgroundColor = Color.cyan;
            _poolExpanded = GUILayout.Toggle(
                _poolExpanded, "▼ Object Pool", "Button");
            GUI.backgroundColor = Color.white;

            if (_poolExpanded)
            {
                GUILayout.BeginVertical("box");
                if (GUILayout.Button("Spawn Drone (G)"))
                {
                    Vector3 pos = new Vector3(Random.Range(-_spawnRange, _spawnRange), 0f, Random.Range(-_spawnRange, _spawnRange));
                    _spawner.SpawnDrone(pos);
                }
                string label = _autoSpawning
                    ? "Stop Auto Spawn (T)"
                    : "Start Auto Spawn (T)";
                if (GUILayout.Button(label))
                    _autoSpawning = !_autoSpawning;

                GUILayout.Space(5);
                GUILayout.Label($"Active {_spawner.GetActiveCount()}");
                GUILayout.Label($"Pooled {_spawner.GetInactiveCount()}");
                GUILayout.EndVertical();
            }


        }

        void DrawObserverSection()
        {
            if (_bikeController == null) return;

            GUI.backgroundColor = Color.magenta;
            _observerExpanded = GUILayout.Toggle(
                _observerExpanded, "▼ Observer Pattern", "button");
            GUI.backgroundColor = Color.white;

            if (_observerExpanded)
            {
                GUILayout.BeginVertical("box");
                if (GUILayout.Button("Take Damage (H)"))
                    _bikeController.TakeDamage(25f);
                if (GUILayout.Button("Toggle Turbo (B)"))
                {
                    if (_bikeController.isTurboActive)
                        _bikeController.DeactivateTurbo();
                    else
                        _bikeController.ActivateTurbo();
                }
                GUILayout.Label($"Health: {_bikeController.health:F0}");
                GUILayout.Label($"Turbo: {(_bikeController.isTurboActive ? "ON" : "OFF")}");
                GUILayout.EndVertical();
            }
        }

        void DrawVisitorSection()
        {
            if (_bikeController == null) return;

            GUI.backgroundColor = Color.red;
            _visitorExpanded = GUILayout.Toggle(
                _visitorExpanded, "▼ Visitor Pattern", "button");
            GUI.backgroundColor = Color.white;

            if (_visitorExpanded)
            {
                GUILayout.BeginVertical("box");
                if (GUILayout.Button("Shield Powerup (V)"))
                    _bikeController.Accept(shieldPowerup);
                if (GUILayout.Button("Engine Powerup (E)"))
                    _bikeController.Accept(enginePowerup);
                if (GUILayout.Button("Weapon Powerup (W)"))
                    _bikeController.Accept(WeaponPowerup);
                GUILayout.EndVertical();
            }
        }

        void DrawStrategySection()
        {
            GUI.backgroundColor = new Color(1f, 0.5f, 0f);
            _strategyExpanded = GUILayout.Toggle(
                _strategyExpanded, "▼ Strategy Pattern", "button");
            GUI.backgroundColor = Color.white;

            if (_strategyExpanded)
            {
                GUILayout.BeginVertical("box");
                if (GUILayout.Button("Spawn Drone (G)"))
                    SpawnDrone();
                GUILayout.EndVertical();
            }
        } 

        Rect resizeHandle = new Rect(_windowRect.width - 15, _windowRect.height - 15, 15, 15);
        GUI.DrawTexture(resizeHandle, Texture2D.whiteTexture);
        EditorGUIUtility.AddCursorRect(
            resizeHandle,
            MouseCursor.ResizeUpLeft);
        if (Event.current.type == EventType.MouseDown && resizeHandle.Contains(Event.current.mousePosition))
            _isResizing = true;

        if (_isResizing)
        {
            _windowRect.width = Mathf.Clamp(Event.current.mousePosition.x, _minSize.x, _maxSize.x);
            _windowRect.height = Mathf.Clamp(Event.current.mousePosition.y, _minSize.y, _maxSize.y);
        }

        if (Event.current.type == EventType.MouseUp)
        {
            _isResizing = false;
        }

        GUI.DragWindow(); // Makes window draggable
    }

    private void ApplyRandomStrategies()
    {
        _components.Add(
            _drone.AddComponent<WeavingManeuver>());
        _components.Add(
            _drone.AddComponent<BoppingManeuver>());
        _components.Add(
            _drone.AddComponent<FallbackManeuver>());
        _components.Add(
            _drone.AddComponent<CirclingManeuver>());

        int index = Random.Range(0, _components.Count);

        _drone.GetComponent<Drone>().ApplyStrategy(_components[index]);
    }
}