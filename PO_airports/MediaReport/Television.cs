using airports_PO.AirportObjects;

namespace airports_PO.MediaReport;

// Television class derived from Media
public class Television : Media
{
    public Television(string name): base(name) { }
    
    public override string Accept(IVisitor visitor, PassengerPlane passengerPlane)
    {
        return visitor.Visit(passengerPlane, this);
    }
    
    public override string Accept(IVisitor visitor, CargoPlane cargoPlane)
    {
        return visitor.Visit(cargoPlane, this);
    }
    
    public override string Accept(IVisitor visitor, Airport airport)
    {
        return visitor.Visit(airport, this);
    }
}