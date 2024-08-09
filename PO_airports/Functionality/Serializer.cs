using System.Text.Json;

namespace airports_PO.Functionality;

// ISerializer interface for any class that implements different types of
// serialization
public interface ISerializer
{
    public string Serialize<T>(T data);
    public string SaveToFile(string data, string path);
}

// JSON_Serializer class implements ISerializer interface and serializes to
// JSON, also can create a file .json
public class SerializerJson : ISerializer
{
    // Serializes data to json and returns the result string
    public string Serialize<T>(T data)
    {
        JsonSerializerOptions opt = new JsonSerializerOptions();
        opt.IncludeFields = true;
        opt.WriteIndented = true;
        string s = JsonSerializer.Serialize(data, opt);
        return s;
    }

    // Saves a data string to json file in location pointed to by path argument
    public string SaveToFile(string data, string path)
    {
        path = path + ".json";
        using StreamWriter sw = new StreamWriter(path);
        sw.Write(data);
        return path;
    }
}
