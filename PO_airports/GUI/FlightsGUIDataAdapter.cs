using airports_PO.AirportObjects;
using airports_PO.Functionality;

namespace airports_PO.GUI;

// This class takes place of and is derived from a provided class FlightsGUIData, which lacked some important
// functionality.
public class FlightsGUIDataAdapter : FlightsGUIData
{
    private List<FlightDecoratorGUI> _flights;
    public readonly object FlightsGUIDataLock = new object();

    public FlightsGUIDataAdapter() : base()
    {
        _flights = new List<FlightDecoratorGUI>();
    }

    public override int GetFlightsCount()
    {
        return _flights.Count;
    }

    public override ulong GetID(int index)
    {
        return _flights[index].Id;
    }

    public override WorldPosition GetPosition(int index)
    {
        return new WorldPosition(_flights[index].Latitude, _flights[index].Longitude);
    }

    public override double GetRotation(int index)
    {
        return _flights[index].Rotation;
    }

    public void UpdateFlightsGUIData()
    {
        lock (FlightsGUIDataLock)
        {
            foreach (var f in _flights)
            {
                f.UpdatePosition();
            }
        }
    }

    public void AddFlightGUIAdapter(Flight flight)
    {
        lock (FlightsGUIDataLock)
        {
            _flights.Add(new FlightDecoratorGUI(flight));
        }
    }
}