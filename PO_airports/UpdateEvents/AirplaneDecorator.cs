using airports_PO.AirportObjects;
using DynamicData;
using NetworkSourceSimulator;

namespace airports_PO.UpdateEvents;

// AirplaneDecorator make it possible for the airplane to keep track of its flights and update them.
public class AirplaneDecorator : Airplane
{
    private List<Flight> _flights;

    public void Subscribe(Flight flight)
    {
        _flights.Add(flight);
    }

    public void Unsubscribe(Flight flight)
    {
        _flights.Add(flight);
    }

    public void Notify(PositionUpdateArgs args, string logsPath)
    {
        foreach (var f in _flights)
        {
            f.Update(args, logsPath);
        }
    }
    
    public AirplaneDecorator(UInt64 id, string serial, string countryIso,
        string model) : base(id, serial, countryIso, model)
    {
        _flights = new List<Flight>();
    }
}