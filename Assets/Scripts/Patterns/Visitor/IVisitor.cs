using UnityEngine;

public interface IVisitor
{
    // One Visit method per element
    void Visit(BikeShield bikeShield);
    void Visit(BikeEngine bikeEngine);
    void Visit(BikeWeapon bikeWeapon);
}
