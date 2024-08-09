using airports_PO.AirportObjects;
using NetworkSourceSimulator;

namespace airports_PO.Factories;

// IFactory interface defines an interface for concrete factories.
public interface IFactory
{
    // Defining constants for sizes of variables in the message
    protected const int InitialOffset = 7;
    protected const int PhoneLength = 12;
    protected const int CargoCodeLength = 6;
    protected const int SerialLength = 10;
    protected const int ISOLength = 3;
    protected const int AirportCodeLength = 3;
    
    // Two create methods work the same but handle two different types of inputs
    public AirportClass Create(string[] line);
    public AirportClass Create(Message message);
}