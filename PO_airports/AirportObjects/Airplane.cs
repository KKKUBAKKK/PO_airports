using System.Text.Json.Serialization;
using NetworkSourceSimulator;

namespace airports_PO.AirportObjects;

// Abstract class derived from AirportClass. Represents an airplane.
public class Airplane : AirportClass
{
    public readonly string Serial;
    public readonly string CountryIso;
    public readonly string Model;

    protected Airplane(UInt64 id, string serial, string countryIso,
        string model) : base(id)
    {
        Serial = serial;
        CountryIso = countryIso;
        Model = model;
    }
}