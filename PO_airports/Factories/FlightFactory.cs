using System.Globalization;
using airports_PO.AirportObjects;
using airports_PO.Functionality;
using NetworkSourceSimulator;

namespace airports_PO.Factories;

// FlightFactory implements IFactory to create an object of type Flight.
public class FlightFactory : IFactory
{
    public AirportClass Create(string[] line)
    {
        UInt64 id = UInt64.Parse(line[1]);
        UInt64 originId = UInt64.Parse(line[2]);
        UInt64 targetId = UInt64.Parse(line[3]);
        string takeoffTime = line[4];
        string landingTime = line[5];
        Single longitude = Single.Parse(line[6],
            CultureInfo.InvariantCulture.NumberFormat);
        Single latitude = Single.Parse(line[7],
            CultureInfo.InvariantCulture.NumberFormat);
        Single amsl = Single.Parse(line[8],
            CultureInfo.InvariantCulture.NumberFormat);
        UInt64 planeId = UInt64.Parse(line[9]);

        string[] temp = line[10].Trim('[', ']').Split(';');
        UInt64[] crewIDs = temp.Select(UInt64.Parse).ToArray();

        temp = line[11].Trim('[', ']').Split(';');
        UInt64[] loadIDs = temp.Select(UInt64.Parse).ToArray();

        return new Flight(id, originId, targetId, takeoffTime, landingTime,
            longitude, latitude, amsl, planeId, crewIDs, loadIDs);
    }

    public AirportClass Create(Message message)
    {
        byte[] args = message.MessageBytes;
        int offset = IFactory.InitialOffset;
        UInt64 id = BitConverter.ToUInt64(args, offset);
        offset += sizeof(UInt64);
        UInt64 originId = BitConverter.ToUInt64(args, offset);
        offset += sizeof(UInt64);
        UInt64 targetId = BitConverter.ToUInt64(args, offset);
        offset += sizeof(UInt64);
        Int64 epochTime = BitConverter.ToInt64(args, offset);
        offset += sizeof(Int64);
        DateTime date = DateTimeOffset.FromUnixTimeMilliseconds(epochTime).DateTime;
        string takeoffTime = date.ToString("HH:mm");
        epochTime = BitConverter.ToInt64(args, offset);
        offset += sizeof(Int64);
        date = DateTimeOffset.FromUnixTimeMilliseconds(epochTime).DateTime;
        string landingTime = date.ToString("HH:mm");
        UInt64 planeId = BitConverter.ToUInt64(args, offset);
        offset += sizeof(UInt64);
        UInt16 crewCount = BitConverter.ToUInt16(args, offset);
        offset += sizeof(UInt16);
        List<UInt64> crewIDs = new List<UInt64>();
        for (int i = 0; i < crewCount; i++)
        {
            crewIDs.Add(BitConverter.ToUInt64(args, offset));
            offset += sizeof(UInt64);
        }
        UInt16 loadCount = BitConverter.ToUInt16(args, offset);
        offset += sizeof(UInt16);
        List<UInt64> loadIDs = new List<UInt64>();
        for (int i = 0; i < loadCount; i++)
        {
            loadIDs.Add(BitConverter.ToUInt64(args, offset));
            offset += sizeof(UInt64);
        }

        Airport airport = Database.Instance.FindAirport(originId);
        float longitude = airport.Longitude;
        float latitude = airport.Latitude;
        float amsl = airport.Amsl;

        return new Flight(id, originId, targetId, takeoffTime, landingTime,
            longitude, latitude, amsl, planeId, crewIDs.ToArray(), loadIDs.ToArray());
    }
}
