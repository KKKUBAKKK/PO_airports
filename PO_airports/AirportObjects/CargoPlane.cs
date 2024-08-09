using System.Text.Json.Serialization;
using airports_PO.Functionality;
using airports_PO.MediaReport;
using airports_PO.UpdateEvents;

namespace airports_PO.AirportObjects;

// Class represents a cargo plane. Derived from AirplaneDecorator and implementing the IReportable interface.
public class CargoPlane : AirplaneDecorator, IReportable
{
    public static string fileId = "CP";
    [JsonInclude] protected readonly Single MaxLoad;

    public CargoPlane(UInt64 id, string serial, string countryIso, string model,
        Single maxLoad) : base(id, serial, countryIso, model)
    {
        MaxLoad = maxLoad;
    }
    
    // Accept method used to implement a visitor, so that I don't have to use reflection.
    public string Accept(IVisitor visitor, Media media)
    {
        return media.Accept(visitor, this);
    }
    
    // AddToDatabase method makes it possible to distinguish data types without reflection.
    public override void AddToDatabase()
    {
        Database.Instance.CargoPlanes.Add(Id, this);
    }
}