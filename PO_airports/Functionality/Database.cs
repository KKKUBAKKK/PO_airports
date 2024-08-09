using System.Drawing.Printing;
using System.Net.Sockets;
using airports_PO.AirportObjects;
using airports_PO.MediaReport;
using airports_PO.UpdateEvents;

namespace airports_PO.Functionality;

// Class database represents a database which contains all the objects created by factories. Database is a singleton.
// It stores objects of each class in corresponding dictionaries where key id the id of an object.
public class Database
{
    private static volatile Database? _instance = null;
    public static readonly object DatabaseLock = new object();

    // As Database is a singleton this is the only way to refer to it. There is no public constructor.
    public static Database Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Database();
            }

            return _instance;
        }
    }

    // Dictionaries for each class. Keys are objects ids
    public Dictionary<ulong, Airport> Airports;
    public Dictionary<ulong, Cargo> Cargos;
    public Dictionary<ulong, CargoPlane> CargoPlanes;
    public Dictionary<ulong, Crew> Crews;
    public Dictionary<ulong, Flight> Flights;
    public Dictionary<ulong, Passenger> Passengers;
    public Dictionary<ulong, PassengerPlane> PassengerPlanes;

    // Protected constructor makes using two databases at the same time impossible.
    protected Database()
    {
        Airports = new Dictionary<ulong, Airport>();
        Cargos = new Dictionary<ulong, Cargo>();
        CargoPlanes = new Dictionary<ulong, CargoPlane>();
        Crews = new Dictionary<ulong, Crew>();
        Flights = new Dictionary<ulong, Flight>();
        Passengers = new Dictionary<ulong, Passenger>();
        PassengerPlanes = new Dictionary<ulong, PassengerPlane>();
    }
    
    
    // Method that changes id of an object (along with its key in a dictionary)
    public void ChangeId(UInt64 id, UInt64 newId)
    {
        lock (DatabaseLock)
        {
            if (Airports.ContainsKey(id))
            {
                var obj = Airports[id];
                Airports.Remove(id);
                obj.Id = newId;
                Airports.Add(newId, obj);
                return;
            }
            if (Cargos.ContainsKey(id))
            {
                var obj = Cargos[id];
                Cargos.Remove(id);
                obj.Id = newId;
                Cargos.Add(newId, obj);
                return;
            }
            if (CargoPlanes.ContainsKey(id))
            {
                var obj = CargoPlanes[id];
                CargoPlanes.Remove(id);
                obj.Id = newId;
                CargoPlanes.Add(newId, obj);
                return;
            }
            if (Crews.ContainsKey(id))
            {
                var obj = Crews[id];
                Crews.Remove(id);
                obj.Id = newId;
                Crews.Add(newId, obj);
                return;
            }
            if (Flights.ContainsKey(id))
            {
                var obj = Flights[id];
                Flights.Remove(id);
                obj.Id = newId;
                Flights.Add(newId, obj);
                return;
            }
            if (Passengers.ContainsKey(id))
            {
                var obj = Passengers[id];
                Passengers.Remove(id);
                obj.Id = newId;
                Passengers.Add(newId, obj);
                return;
            }
            if (PassengerPlanes.ContainsKey(id))
            {
                var obj = PassengerPlanes[id];
                PassengerPlanes.Remove(id);
                obj.Id = newId;
                PassengerPlanes.Add(newId, obj);
                return;
            }
        }

        throw (new KeyNotFoundException($"No object with id: {id} to be changed in database"));
    }
    
    // Method lets user find a flight by id. It returns the desired flight or THROWS a KeyNotFoundException!!!
    public Flight FindFlight(UInt64 id)
    {
        if (Flights.ContainsKey(id))
            return Flights[id];
        throw new KeyNotFoundException($"No flight with Id: {id} in database");
    }

    // Method lets user find an airport by id. It returns the desired airport or THROWS a KeyNotFoundException!!!
    public Airport FindAirport(UInt64 id)
    {
        if (Airports.ContainsKey(id))
            return Airports[id];
        throw new KeyNotFoundException($"No airport with Id: {id} in database");
    }

    // Method lets user find a AirplaneDecorator by id. It returns the desired object or THROWS a KeyNotFoundException!!!
    public AirplaneDecorator FindAirplane(UInt64 id)
    {
        if (CargoPlanes.ContainsKey(id))
            return CargoPlanes[id];
        if (PassengerPlanes.ContainsKey(id))
            return PassengerPlanes[id];
        throw new KeyNotFoundException($"No airplane with Id: {id} in database");
    }

    // Method lets user find a Crew by id. It returns the desired object or THROWS a KeyNotFoundException!!!
    public Crew FindCrew(UInt64 id)
    {
        if (Crews.ContainsKey(id))
            return Crews[id];
        throw new KeyNotFoundException($"No crew with Id: {id} in database");
    }

    // Method lets user find Passenger or Cargo by id. It returns the desired object or THROWS a KeyNotFoundException!!!
    public AirportClass FindLoad(UInt64 id)
    {
        if (Passengers.ContainsKey(id))
            return Passengers[id];
        if (Cargos.ContainsKey(id))
            return Cargos[id];
        throw new KeyNotFoundException($"No load with Id: {id} in database");
    }

    // Method lets user find an IReportable by id. It returns the desired object or THROWS a KeyNotFoundException!!!
    public IReportable FindReportable(UInt64 id)
    {
        if (Airports.ContainsKey(id))
            return Airports[id];
        if (PassengerPlanes.ContainsKey(id))
            return PassengerPlanes[id];
        if (CargoPlanes.ContainsKey(id))
            return CargoPlanes[id];
        throw new KeyNotFoundException($"No reportable with Id: {id} in database");
    }

    // Method lets user find a Person by id. It returns the desired object or THROWS a KeyNotFoundException!!!
    public Person FindPerson(UInt64 id)
    {
        if (Crews.ContainsKey(id))
            return Crews[id];
        if (Passengers.ContainsKey(id))
            return Passengers[id];
        throw new KeyNotFoundException($"No person with Id: {id} in database");
    }
}