using UnityEngine;

public class BikeStopState :
    MonoBehaviour, IBikeState
{
    private BikeController
        _bikeController;

    public void Handle(
        BikeController bikeController)
    {
        // Cache reference
        if (!_bikeController)
            _bikeController = bikeController;

        // STOP: set speed to zero
        _bikeController.CurrentSpeed = 0;

        Debug.Log("[BikeStopState] Stopped");
    }
}