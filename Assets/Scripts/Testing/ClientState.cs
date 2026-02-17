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
    }
}