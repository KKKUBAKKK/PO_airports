using airports_PO.Functionality;
using airports_PO.MediaReport;
using airports_PO.UpdateEvents;

namespace airports_PO.AirportObjects;

// Class represents a passenger plane. Derived from Airplane.
public class PassengerPlane : AirplaneDecorator, IReportable
{
    public static string fileId = "PP";
    public readonly UInt16 FirstClassSize;
    public readonly UInt16 BusinessClassSize;
    public readonly UInt16 EconomyClassSize;

    public PassengerPlane(UInt64 id, string serial, string countryIso,
        string model, UInt16 firstClassSize, UInt16 businessClassSize,
        UInt16 economyClassSize) : base(id, serial, countryIso, model)
    {
        FirstClassSize = firstClassSize;
        BusinessClassSize = businessClassSize;
        EconomyClassSize = economyClassSize;
    }

    // Accept method used to implement a visitor, so that I don't have to use reflection.
    public string Accept(IVisitor visitor, Media media)
    {
        return media.Accept(visitor, this);
    }
    
    // AddToDatabase method makes it possible to distinguish data types without reflection.
    public override void AddToDatabase()
    {
        Database.Instance.PassengerPlanes.Add(Id, this);
    }
}