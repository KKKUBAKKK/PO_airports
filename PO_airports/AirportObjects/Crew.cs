using System.Text.Json.Serialization;
using airports_PO.Functionality;

namespace airports_PO.AirportObjects;

// Class represents a member of the crew. Derived from Person.
public class Crew : Person
{
    public static string fileId = "C";
    [JsonInclude] protected UInt16 Practice;
    [JsonInclude] protected string Role;

    public Crew(UInt64 id, string name, UInt64 age, string phone, string email,
        UInt16 practice, string role) :
        base(id, name, age, phone, email)
    {
        Practice = practice;
        Role = role;
    }
    
    // AddToDatabase method makes it possible to distinguish data types without reflection.
    public override void AddToDatabase()
    {
        Database.Instance.Crews.Add(Id, this);
    }
}