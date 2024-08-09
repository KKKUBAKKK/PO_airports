using airports_PO.AirportObjects;
using airports_PO.Factories;
using airports_PO.GUI;
using airports_PO.MediaReport;

namespace airports_PO.Functionality;

// DataReader is a class that lets you read data from as file line-by-line
// into a list of objects of type DataType
public class DataReader
{
    private readonly Factory _factory;
    
    // Constructor
    public DataReader(Factory factory)
    {
        _factory = factory;
    }

    public DataReader()
    {
        _factory = new Factory();
    }
    
    // Method reads data from and .ftr file line-by-line and creates corresponding objects, then adds them to the list
	public List<AirportClass> ReadToList(string inFilePath)
	{
        // List of objects read from file
        var data = new List<AirportClass>();

        // StreamReader to a file specified by inFilePath
        using StreamReader reader = new StreamReader(inFilePath);

        // Reading line-by-line and adding created objects to the list
        string[]? line = reader.ReadLine()?.Split(',');
        while (line != null)
        {
            try
            {
                AirportClass obj = _factory.Create(line);
                data.Add(obj);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            line = reader.ReadLine()?.Split(',');
        }

        return data;
    }
    
    // Method reads data from and .ftr file line-by-line and creates corresponding objects, then adds them to the dictionary
    public Dictionary<string, Dictionary<UInt64, AirportClass>> ReadToDict(string inFilePath)
    {
        // List of objects read from file
        var data = new Dictionary<string, Dictionary<UInt64, AirportClass>>();

        // StreamReader to a file specified by inFilePath
        using StreamReader reader = new StreamReader(inFilePath);

        // Reading line-by-line and adding created objects to the list
        string[]? line = reader.ReadLine()?.Split(',');
        while (line != null)
        {
            try
            {
                AirportClass obj = _factory.Create(line);
                if (data.ContainsKey(line[0]))
                    data[line[0]].Add(obj.Id, obj);
                else
                {
                    data[line[0]] = new Dictionary<UInt64, AirportClass>();
                    data[line[0]].Add(obj.Id, obj);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            line = reader.ReadLine()?.Split(',');
        }

        return data;
    }
    
    public void ReadToDatabase(string inFilePath)
    {
        // StreamReader to a file specified by inFilePath
        using StreamReader reader = new StreamReader(inFilePath);

        // Reading line-by-line and adding created objects to the list
        string[]? line = reader.ReadLine()?.Split(',');
        while (line != null)
        {
            try
            {
                AirportClass obj = _factory.Create(line);
                obj.AddToDatabase();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            line = reader.ReadLine()?.Split(',');
        }
    }
    
    public void ReadToDatabase(string inFilePath, FlightsGUIDataAdapter flightsGuiDataAdapter, UserCommands userCommands)
    {
        // StreamReader to a file specified by inFilePath
        using StreamReader reader = new StreamReader(inFilePath);

        // Reading line-by-line and adding created objects to the list
        string[]? line = reader.ReadLine()?.Split(',');
        while (line != null)
        {
            try
            {
                AirportClass obj = _factory.Create(line);
                obj.AddToDatabase();
                if (line[0] == Flight.fileId)
                    flightsGuiDataAdapter.AddFlightGUIAdapter((Flight) obj);
                if (obj is IReportable reportable)
                    userCommands.AddReportable(reportable);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            line = reader.ReadLine()?.Split(',');
        }
    }
}
