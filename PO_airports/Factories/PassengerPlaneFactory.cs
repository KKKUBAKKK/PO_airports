using System.Text;
using airports_PO.AirportObjects;
using NetworkSourceSimulator;

namespace airports_PO.Factories;

// PassengerPlaneFactory implements IFactory to create an object of type PassengerPlane.
public class PassengerPlaneFactory : IFactory
{
    public AirportClass Create(string[] line)
    {
        UInt64 id = UInt64.Parse(line[1]);
        string serial = line[2];
        string countryIso = line[3];
        string model = line[4];
        UInt16 firstClassSize = UInt16.Parse(line[5]);
        UInt16 businessClassSize = UInt16.Parse(line[6]);
        UInt16 economyClassSize = UInt16.Parse(line[7]);

        return new PassengerPlane(id, serial, countryIso, model, firstClassSize,
            businessClassSize, economyClassSize);
    }

    public AirportClass Create(Message message)
    {
        byte[] args = message.MessageBytes;
        int offset = IFactory.InitialOffset;
        UInt64 id = BitConverter.ToUInt64(args, offset);
        offset += sizeof(UInt64);
        string serial = Encoding.ASCII.GetString(args, offset, IFactory.SerialLength);
        offset += IFactory.SerialLength;
        string countryIso = Encoding.ASCII.GetString(args, offset, IFactory.ISOLength);
        offset += IFactory.ISOLength;
        UInt16 modelLength = BitConverter.ToUInt16(args, offset);
        offset += sizeof(UInt16);
        string model = Encoding.ASCII.GetString(args, offset, modelLength);
        offset += modelLength;
        UInt16 firstClassSize = BitConverter.ToUInt16(args, offset);
        offset += sizeof(UInt16);
        UInt16 businessClassSize = BitConverter.ToUInt16(args, offset);
        offset += sizeof(UInt16);
        UInt16 economyClassSize = BitConverter.ToUInt16(args, offset);

        return new PassengerPlane(id, serial, countryIso, model, firstClassSize,
            businessClassSize, economyClassSize);
    }
}
