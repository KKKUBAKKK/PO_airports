using airports_PO.AirportObjects;
using NetworkSourceSimulator;

namespace airports_PO.Updates;

// TODO: probably delete
public class AirplaneDecorator : Airplane
{
    public delegate void PositionUpdateHandler(PositionUpdateArgs args); 
    public event EventHandler? PositionUpdate;

    protected virtual void OnPositionUpdate()
    {
        PositionUpdate?.Invoke(this, EventArgs.Empty);
    }
    
    protected AirplaneDecorator(UInt64 id, string serial, string countryIso,
        string model) : base(id, serial, countryIso, model) { }
}