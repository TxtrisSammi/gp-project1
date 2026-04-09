using UnityEngine;

public class ClientState : MonoBehaviour
{
    private BikeController _bikeController;

    void Start()
    {
        // Find BikeController in scene
        _bikeController = FindFirstObjectByType<BikeController>();
    }

    void OnGUI()
    {
        // GUI buttons
        if (GUILayout.Button("Start Bike"))
        _bikeController.StartBike();
        if (GUILayout.Button("Turn Left"))
        _bikeController.Turn(Direction.Left);
        if (GUILayout.Button("Turn Right"))
        _bikeController.Turn(Direction.Right);
        if (GUILayout.Button("Stop Bike"))
        _bikeController.StopBike();
        if (GUILayout.Button("Crash Bike"))
        _bikeController.CrashBike();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        _bikeController.StartBike();
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        _bikeController.Turn(Direction.Left);
        if (Input.GetKeyDown(KeyCode.RightArrow))
        _bikeController.Turn(Direction.Right);
        if (Input.GetKeyDown(KeyCode.S))
        _bikeController.StopBike();
        if (Input.GetKeyDown(KeyCode.C))
        _bikeController.CrashBike();
    }
}