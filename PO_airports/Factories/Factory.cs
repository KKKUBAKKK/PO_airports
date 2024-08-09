using System.Text;
using airports_PO.AirportObjects;
using NetworkSourceSimulator;

namespace airports_PO.Factories;

// Factory class decides which concrete factory to use to create an instance of an object, based on a dictionary
public class Factory
{
    private readonly Dictionary<string, IFactory> _factories;

    public Factory()
    {
        _factories = new Dictionary<string, IFactory>();
        _factories[Crew.fileId] = new CrewFactory();
        _factories[Passenger.fileId] = new PassengerFactory();
        _factories[Cargo.fileId] = new CargoFactory();
        _factories[CargoPlane.fileId] = new CargoPlaneFactory();
        _factories[PassengerPlane.fileId] = new PassengerPlaneFactory();
        _factories[Airport.fileId] = new AirportFactory();
        _factories[Flight.fileId] = new FlightFactory();
    }

    // Method picks the right factory and creates an object based on an array of strings
    public AirportClass Create(string[] line)
    {
        return _factories[line[0]].Create(line);
    }

    // Method picks the right factory and creates an object based on a Message object
    public AirportClass Create(Message message)
    {
        var key = GetKey(message);
        return _factories[key].Create(message);
    }

    // This method is used to extract a key to the dictionary from a message
    public string GetKey(Message message)
    {
        var key = Encoding.ASCII.GetString(message.MessageBytes, 1, 2);
        if (key == "CR")
            key = Crew.fileId;
        else if (key == "PA")
            key = Passenger.fileId;
        return key;
    }
}