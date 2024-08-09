using System.Text.Json.Serialization;
using airports_PO.Functionality;

namespace airports_PO.AirportObjects;

// Class represents a passenger. Derived from Person.
public class Passenger : Person
{
    public static string fileId = "P";
    [JsonInclude] protected string Class;
    [JsonInclude] protected UInt64 Miles;

    public Passenger(UInt64 id, string name, UInt64 age, string phone,
        string email, string @class, UInt64 miles) :
        base(id, name, age, phone, email)
    {
        Class = @class;
        Miles = miles;
    }
    
    // AddToDatabase method makes it possible to distinguish data types without reflection.
    public override void AddToDatabase()
    {
        Database.Instance.Passengers.Add(Id, this);
    }
}