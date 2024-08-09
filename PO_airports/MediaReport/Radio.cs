using airports_PO.AirportObjects;

namespace airports_PO.MediaReport;

// Radio class derived from Media
public class Radio : Media
{
    public Radio(string name): base(name) { }
    
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