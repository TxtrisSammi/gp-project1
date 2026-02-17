using UnityEngine;

public class BikeTurnState :
    MonoBehaviour, IBikeState
{
    private Vector3 _turnDirection;
    private BikeController _bikeController;

    public void Handle(
        BikeController bikeController)
    {
        if (!_bikeController)
            _bikeController = bikeController;

        // Set direction (Left=-1, Right=1)
        _turnDirection.x = (float)
            _bikeController.CurrentTurnDirection;

        // Only turn if moving
        if (_bikeController.CurrentSpeed > 0)
        {
            transform.Translate(_turnDirection *
                                _bikeController.turnDistance);
        }
    }
}