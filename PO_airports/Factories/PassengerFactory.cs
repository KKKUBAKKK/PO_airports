using System.Text;
using airports_PO.AirportObjects;
using NetworkSourceSimulator;

namespace airports_PO.Factories;

// PassengerFactory implements IFactory interface to create an object of type Passenger.
public class PassengerFactory : IFactory
{
    public AirportClass Create(string[] line)
    {
        UInt64 id = UInt64.Parse(line[1]);
        string name = line[2];
        UInt64 age = UInt64.Parse(line[3]);
        string phone = line[4];
        string email = line[5];
        string @class = line[6];
        UInt64 miles = UInt64.Parse(line[7]);

        return new Passenger(id, name, age, phone, email, @class, miles);
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
        UInt64 age = BitConverter.ToUInt16(args, offset);
        offset += sizeof(UInt16);
        string phone = Encoding.ASCII.GetString(args, offset, IFactory.PhoneLength);
        offset += IFactory.PhoneLength;
        UInt16 emailLength = BitConverter.ToUInt16(args, offset);
        offset += sizeof(UInt16);
        string email = Encoding.ASCII.GetString(args, offset, emailLength);
        offset += emailLength;
        char c = (char)args[offset];
        offset += 1;
        string @class;
        if (c == 'F')
            @class = "First";
        else if (c == 'B')
            @class = "Business";
        else
            @class = "Economic";
        UInt64 miles = BitConverter.ToUInt64(args, offset);

        return new Passenger(id, name, age, phone, email, @class, miles);
    }
}