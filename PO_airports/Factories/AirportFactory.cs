using System.Globalization;
using System.Text;
using airports_PO.AirportObjects;
using NetworkSourceSimulator;

namespace airports_PO.Factories;

// AirportFactory implements IFactory to create an object of type Airport.
public class AirportFactory : IFactory
{
    public AirportClass Create(string[] line)
    {
        UInt64 id = UInt64.Parse(line[1]);
        string name = line[2];
        string code = line[3];
        Single longitude = Single.Parse(line[4],
            CultureInfo.InvariantCulture.NumberFormat);
        Single latitude = Single.Parse(line[5],
            CultureInfo.InvariantCulture.NumberFormat);
        Single amsl = Single.Parse(line[6],
            CultureInfo.InvariantCulture.NumberFormat);
        string countryIso = line[7];

        return new Airport(id, name, code, longitude, latitude, amsl, countryIso);
    }

    public AirportClass Create(Message message)
    {
        byte[] args = message.MessageBytes;
        int offset = IFactory.InitialOffset;
        UInt64 id = BitConverter.ToUInt64(args, offset);
        offset += sizeof(UInt64);
        UInt16 nameLength = BitConverter.ToUInt16(args, offset);
        offset += sizeof(UInt16);
        string name = Encoding.ASCII.GetString(args, offset, nameLength);
        offset += nameLength;
        string code = Encoding.ASCII.GetString(args, offset, IFactory.AirportCodeLength);
        offset += IFactory.AirportCodeLength;
        Single longitude = BitConverter.ToSingle(args, offset);
        offset += sizeof(Single);
        Single latitude = BitConverter.ToSingle(args, offset);
        offset += sizeof(Single);
        Single amsl = BitConverter.ToSingle(args, offset);
        offset += sizeof(Single);
        string countryIso = Encoding.ASCII.GetString(args, offset, IFactory.ISOLength);

        return new Airport(id, name, code, longitude, latitude, amsl, countryIso);
    }
}