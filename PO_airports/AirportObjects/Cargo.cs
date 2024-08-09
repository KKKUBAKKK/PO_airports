using System.Text.Json.Serialization;
using airports_PO.Functionality;

namespace airports_PO.AirportObjects;

// Class represents cargo. Derived from AirportClass.
public class Cargo : AirportClass
{
    public static string fileId = "CA";
    [JsonInclude] protected readonly Single Weight;
    public readonly string Code;
    [JsonInclude] protected readonly string Description;

    public Cargo(UInt64 id, Single weight, string code, string description) :
        base(id)
    {
        Weight = weight;
        Code = code;
        Description = description;
    }
    
    // AddToDatabase method makes it possible to distinguish data types without reflection.

    public override void AddToDatabase()
    {
        Database.Instance.Cargos.Add(Id, this);
    }
}