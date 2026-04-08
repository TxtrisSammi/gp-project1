/// The state interface
public interface IBikeState
{
    // Handle state behavior
    void Handle(BikeController controller);
    // Check if Bike Crashed
    bool IsCrashed();
}