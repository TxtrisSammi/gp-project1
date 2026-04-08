using UnityEngine;

public class BikeCrashState : MonoBehaviour, IBikeState
{
    private BikeController _bikeController;

    public void Handle(BikeController bikeController)
    {
        if (!_bikeController)
            _bikeController = bikeController;

        _bikeController.CurrentSpeed = 0;
        Debug.Log("Bike crashed!");
    }

    public bool IsCrashed()
    {
        return true;
    }
}