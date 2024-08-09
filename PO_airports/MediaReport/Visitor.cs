using airports_PO.AirportObjects;

namespace airports_PO.MediaReport;

// Visitor class implements news from each Media type about different classes.
public class Visitor : IVisitor
{
    public string Visit(PassengerPlane passengerPlane, Television television)
    {
        return $"<An image of {passengerPlane.Model}: {passengerPlane.Serial} passenger plane>";
    }

    public string Visit(CargoPlane cargoPlane, Television television)
    {
        return $"<An image of {cargoPlane.Model}: {cargoPlane.Serial} cargo plane>";
    }

    public string Visit(Airport airport, Television television)
    {
        return $"<An image of {airport.Name} airport >";
    }

    public string Visit(PassengerPlane passengerPlane, Radio radio)
    {
        return $"Reporting for {radio.Name}, Ladies and gentlemen, we’ve just witnessed {passengerPlane.Serial} takeoff.";
    }

    public string Visit(CargoPlane cargoPlane, Radio radio)
    {
        return $"Reporting for {radio.Name}, Ladies and gentlemen, we are seeing the {cargoPlane.Serial} aircraft fly above us.";
    }

    public string Visit(Airport airport, Radio radio)
    {
        return $"Reporting for {radio.Name}, Ladies and gentlemen, we are at the {airport.Name} airport.";
    }

    public string Visit(PassengerPlane passengerPlane, Newspaper newspaper)
    {
        return $"{newspaper.Name} - Breaking news! {passengerPlane.Model} aircraft loses EASA fails certification after inspection of {passengerPlane.Serial}.";
    }

    public string Visit(CargoPlane cargoPlane, Newspaper newspaper)
    {
        return $"{newspaper.Name} -An interview with the crew of {cargoPlane.Serial}.";
    }

    public string Visit(Airport airport, Newspaper newspaper)
    {
        return $"{newspaper.Name} - A report from the {airport.Name} airport, {airport.CountryIso}.";
    }
}