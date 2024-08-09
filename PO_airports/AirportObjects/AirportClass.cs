using System.Text.Json.Serialization;

namespace airports_PO.AirportObjects;

// Setting JsonDerivedType attributes for AirportClass
[JsonDerivedType(typeof(Person), typeDiscriminator: "Person")]
[JsonDerivedType(typeof(Airplane), typeDiscriminator: "Airplane")]
[JsonDerivedType(typeof(Crew), typeDiscriminator: "Crew")]
[JsonDerivedType(typeof(Passenger), typeDiscriminator: "Passenger")]
[JsonDerivedType(typeof(Cargo), typeDiscriminator: "Cargo")]
[JsonDerivedType(typeof(CargoPlane), typeDiscriminator: "CargoPlane")]
[JsonDerivedType(typeof(PassengerPlane), typeDiscriminator: "PassengerPlane")]
[JsonDerivedType(typeof(Airport), typeDiscriminator: "Airport")]
[JsonDerivedType(typeof(Flight), typeDiscriminator: "Flight")]

// Abstract class used as a base class for many classes in this project, every object has an id.
public class AirportClass
{
    public UInt64 Id;

    protected AirportClass(UInt64 id)
    {
        Id = id;
    }
    
    public virtual void AddToDatabase() { }
}