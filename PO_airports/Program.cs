using airports_PO.Functionality;
using airports_PO.GUI;
using FlightTrackerGUI;

namespace airports_PO;

static class Program
{
    static void Main()
    {
        // Paths to the files
        const string ftrFilePath = "../../../Resources/example_data.ftr";
        const string ftreFilePath = "../../../Resources/example.ftre";
        string logsPath = $"../../../Logs/{DateTime.Now.ToString("dd-MM-yyyy_HH:mm:ss")}_log.txt";
        const string outFilePath = "../../../Snapshots/snapshot";
        
        // Offsets for the server
        const int minOffset = 100;
        const int maxOffset = 500;
        
        // Creating instances of objects needed 
        var netSimulator = new NetworkSourceSimulator.NetworkSourceSimulator(ftreFilePath, minOffset, maxOffset);
        ISerializer serializer = new SerializerJson();
        DataReader dataReader = new DataReader();
        DataReceiver dataReceiver = new DataReceiver(netSimulator);
        UserCommands userCommands = new UserCommands(serializer);
        ManagerGUI managerGui = new ManagerGUI();
        
        // Reading data from ftr file to database
        dataReader.ReadToDatabase(ftrFilePath, managerGui.FlightsGui, userCommands);
        
        // Starting the server
        dataReceiver.RunSimulator(logsPath);
        userCommands.RunGetUserCommandsThread(outFilePath);
        
        // Opening and updating GUI
        managerGui.RunFlightManagerThread();
        Runner.Run();
    }
}
