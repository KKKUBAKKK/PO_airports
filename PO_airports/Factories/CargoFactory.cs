using System.Globalization;
using System.Text;
using airports_PO.AirportObjects;
using NetworkSourceSimulator;

namespace airports_PO.Factories;

// CargoFactory implements IFactory interface to create an object of type Cargo.
public class CargoFactory : IFactory
{
    public AirportClass Create(string[] line)
    {
        UInt64 id = UInt64.Parse(line[1]);
        Single weight = Single.Parse(line[2],
            CultureInfo.InvariantCulture.NumberFormat);
        string code = line[3];
        string description = line[4];

        return new Cargo(id, weight, code, description);
    }

    public AirportClass Create(Message message)
    {
        byte[] args = message.MessageBytes;
        int offset = IFactory.InitialOffset;
        UInt64 id = BitConverter.ToUInt64(args, offset);
        offset += sizeof(UInt64);
        Single weight = BitConverter.ToSingle(args, offset);
        offset += sizeof(Single);
        string code = Encoding.ASCII.GetString(args, offset, IFactory.CargoCodeLength);
        offset += IFactory.CargoCodeLength;
        UInt16 descriptionLength = BitConverter.ToUInt16(args, offset);
        offset += sizeof(UInt16);
        string description = Encoding.ASCII.GetString(args, offset, descriptionLength);

        return new Cargo(id, weight, code, description);
    }
}