using System.Text.Json.Serialization;
using airports_PO.Functionality;
using airports_PO.UpdateEvents;
using NetworkSourceSimulator;

namespace airports_PO.AirportObjects;

// Class represents a flight. Derived from AirportClass.
public class Flight : AirportClass
{
    public const string fileId = "FL";

    [JsonInclude]
    public UInt64 OriginId
    {
        get { return Origin.Id; }
    }
    [JsonIgnore]
    public Airport Origin;

    [JsonInclude]
    public UInt64 TargetId
    {
        get { return Target.Id; }
    }
    [JsonIgnore]
    public Airport Target;
    
    [JsonInclude] public readonly string TakeoffTime;
    [JsonInclude] public readonly string LandingTime;
    [JsonInclude] public Single Longitude;
    [JsonInclude] public Single Latitude;
    [JsonInclude] public Single Amsl;

    [JsonInclude]
    protected UInt64 PlaneId
    {
        get { return Plane.Id; }
    }
    [JsonIgnore]
    public AirplaneDecorator Plane;

    [JsonInclude]
    protected UInt64[] CrewIDs
    {
        get { return Crew.Select(c => c.Id).ToArray(); }
    }
    [JsonIgnore]
    public Crew[] Crew;

    [JsonInclude]
    protected UInt64[] LoadIDs
    {
        get { return Load.Select(l => l.Id).ToArray(); }
    }
    [JsonIgnore]
    public AirportClass[] Load;

    public Flight(UInt64 id, UInt64 originId, UInt64 targetId,
        string takeoffTime, string landingTime, Single longitude,
        Single latitude, Single amsl, UInt64 planeId, UInt64[] crewIDs,
        UInt64[] loadIDs) : base(id)
    {
        TakeoffTime = takeoffTime;
        LandingTime = landingTime;
        Longitude = longitude;
        Latitude = latitude;
        Amsl = amsl;

        Origin = Database.Instance.FindAirport(originId);
        Target = Database.Instance.FindAirport(targetId);
        Plane = Database.Instance.FindAirplane(planeId);
        Plane.Subscribe(this);
        
        Crew = new Crew[crewIDs.Length];
        for (int i = 0; i < crewIDs.Length; i++)
        {
            Crew[i] = Database.Instance.FindCrew(crewIDs[i]);
        }

        Load = new AirportClass[loadIDs.Length];
        for (int i = 0; i < Load.Length; i++)
        {
            Load[i] = Database.Instance.FindLoad(loadIDs[i]);
        }
    }

    // Update handles updating the current location of the flight.
    public void Update(PositionUpdateArgs args, string logsPath)
    {
        DataReceiver.AppendToLog(logsPath, $"{DateTime.Now} - Flight with ID: {args.ObjectID} position changed\n" +
                              $"\t Longitude: {Longitude} -> {args.Longitude}\n" +
                              $"\t Latitude: {Latitude} -> {args.Latitude}\n" +
                              $"\t AMSL: {Amsl} -> {args.AMSL}");
        
        Longitude = args.Longitude;
        Latitude = args.Latitude;
        Amsl = args.AMSL;
    }
    
    // AddToDatabase method makes it possible to distinguish data types without reflection.
    public override void AddToDatabase()
    {
        Database.Instance.Flights.Add(Id, this);
    }
}
