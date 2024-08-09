using System.Text;
using airports_PO.AirportObjects;
using NetworkSourceSimulator;

namespace airports_PO.Factories;

// CrewFactory implements IFactory interface to create an object of type Crew.
public class CrewFactory : IFactory
{
    public AirportClass Create(string[] line)
    {
        UInt64 id = UInt64.Parse(line[1]);
        string name = line[2];
        UInt64 age = UInt64.Parse(line[3]);
        string phone = line[4];
        string email = line[5];
        UInt16 practice = UInt16.Parse(line[6]);
        string role = line[7];

        return new Crew(id, name, age, phone, email, practice, role);
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
        UInt16 practice = BitConverter.ToUInt16(args, offset);
        offset += sizeof(UInt16);
        char r = (char) args[offset];
        string role;
        if (r == 'C')
            role = "Captain";
        else if (r == 'A')
            role = "Attendant";
        else
            role = "Other";

        return new Crew(id, name, age, phone, email, practice, role);
    }
}