using UnityEngine;

public class BikeStartState :
    MonoBehaviour, IBikeState
{
    private BikeController _bikeController;

    public void Handle(
        BikeController bikeController)
    {
        if (!_bikeController)
            _bikeController = bikeController;

        // START: set to max speed
        _bikeController.CurrentSpeed =
            _bikeController.maxSpeed;
    }

    void Update()
    {
        // Move forward each frame
        if (_bikeController)
        {
            if (_bikeController.CurrentSpeed > 0)
            {
                _bikeController.transform.Translate(
                    Vector3.forward *
                    (_bikeController.CurrentSpeed *
                     Time.deltaTime));
            }
        }
    }
}