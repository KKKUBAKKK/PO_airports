using airports_PO.AirportObjects;

namespace airports_PO.MediaReport;

// Abstract class Media is just a base class for media channels, like television, radio and newspaper
public abstract class Media
{
    public readonly string Name;

    public Media(string name)
    {
        Name = name;
    }

    public abstract string Accept(IVisitor visitor, PassengerPlane passengerPlane);
    public abstract string Accept(IVisitor visitor, CargoPlane cargoPlane);
    public abstract string Accept(IVisitor visitor, Airport airport);
}