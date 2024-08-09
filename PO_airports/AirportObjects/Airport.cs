using airports_PO.Functionality;
using airports_PO.MediaReport;

namespace airports_PO.AirportObjects;

// Class represents an airport. Derived from AirportClass and implementing IReportable interface.
public class Airport : AirportClass, IReportable
{
    public static string fileId = "AI";
    public readonly string Name;
    public readonly string Code;
    public readonly Single Longitude;
    public readonly Single Latitude;
    public readonly Single Amsl;
    public readonly string CountryIso;

    public Airport(UInt64 id, string name, string code, Single longitude,
        Single latitude, Single amsl, string countryIso) : base(id)
    {
        Name = name;
        Code = code;
        Longitude = longitude;
        Latitude = latitude;
        Amsl = amsl;
        CountryIso = countryIso;
    }
    
    // Accept method used to implement a visitor, so that I don't have to use reflection.
    public string Accept(IVisitor visitor, Media media)
    {
        return media.Accept(visitor, this);
    }

    // AddToDatabase method makes it possible to distinguish data types without reflection.
    public override void AddToDatabase()
    {
        Database.Instance.Airports.Add(Id, this);
    }
}