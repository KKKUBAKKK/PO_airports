using System.Text.Json.Serialization;

namespace airports_PO.AirportObjects;

// Abstract class derived from DataType. Represents Person.
public class Person : AirportClass
{
    [JsonInclude] protected readonly string Name;
    [JsonInclude] protected UInt64 Age;
    [JsonInclude] protected string Phone;
    [JsonInclude] protected string Email;

    protected Person(UInt64 id, string name, UInt64 age, string phone,
        string email) : base(id)
    {
        Name = name;
        Age = age;
        Phone = phone;
        Email = email;
    }
    
    public string GetEmail { get { return Email; } }
    public string GetPhone { get { return Phone; } }
    
    // This method handles updates of contact info.
    public void UpdateContactInfo(string phone, string email)
    {
        Phone = phone;
        Email = email;
    }
}