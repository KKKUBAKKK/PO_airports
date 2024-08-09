using airports_PO.AirportObjects;

namespace airports_PO.MediaReport;

// Visitor makes reporting news with different classes without using reflection possible.
public interface IVisitor
{
    public string Visit(PassengerPlane passengerPlane, Television television);
    public string Visit(CargoPlane cargoPlane, Television television);
    public string Visit(Airport airport, Television television);
    
    public string Visit(PassengerPlane passengerPlane, Radio radio);
    public string Visit(CargoPlane cargoPlane, Radio radio);
    public string Visit(Airport airport, Radio radio);
    
    public string Visit(PassengerPlane passengerPlane, Newspaper newspaper);
    public string Visit(CargoPlane cargoPlane, Newspaper newspaper);
    public string Visit(Airport airport, Newspaper newspaper);
}