using System.Globalization;
using System.Text;
using airports_PO.AirportObjects;
using NetworkSourceSimulator;

namespace airports_PO.Factories;

// CargoPlaneFactory implements IFactory interface to create an object of type CargoPlaneFactory.
public class CargoPlaneFactory : IFactory
{
    public AirportClass Create(string[] line)
    {
        UInt64 id = UInt64.Parse(line[1]);
        string serial = line[2];
        string countryIso = line[3];
        string model = line[4];
        Single maxLoad = Single.Parse(line[5],
            CultureInfo.InvariantCulture.NumberFormat);

        return new CargoPlane(id, serial, countryIso, model, maxLoad);
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
        Single maxLoad = BitConverter.ToSingle(args, offset);

        return new CargoPlane(id, serial, countryIso, model, maxLoad);
    }
}