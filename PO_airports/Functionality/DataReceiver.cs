using System.Text;
using airports_PO.AirportObjects;
using airports_PO.Factories;
using airports_PO.GUI;
using airports_PO.MediaReport;
using airports_PO.UpdateEvents;
using Avalonia.Remote.Protocol.Input;
using FlightTrackerGUI;

namespace airports_PO.Functionality;

// DataReceiver class is used to simulate the work of a network source and handle messages from it
public class DataReceiver
{
    public readonly NetworkSourceSimulator.NetworkSourceSimulator NetSimulator;
    public readonly Factory ObjFactory;
    
    // Basic constructor
    public DataReceiver(NetworkSourceSimulator.NetworkSourceSimulator netSimulator)
    {
        NetSimulator = netSimulator;
        ObjFactory = new Factory();
    }

    // Method adds a function that handles NewDataReady event to OnNewDataReady in the NetSimulator
    // The added function creates and object corresponding to the one in the message
    // This method should be invoked before RunSimulator()
    public void CreateOnNewDataReadyEvent(List<AirportClass> data)
    {
        NetSimulator.OnNewDataReady += (sender, args) =>
        {
            try
            {
                var message = NetSimulator.GetMessageAt(args.MessageIndex);
                var obj = ObjFactory.Create(message);
                data.Add(obj);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        };
    }
    
    // Method adds a function that handles NewDataReady event to OnNewDataReady in the NetSimulator
    // The added function creates and object corresponding to the one in the message
    // This method should be invoked before RunSimulator()
    public void CreateOnNewDataReadyEvent()
    {
        NetSimulator.OnNewDataReady += (sender, args) =>
        {
            try
            {
                var message = NetSimulator.GetMessageAt(args.MessageIndex);
                var obj = ObjFactory.Create(message);
                obj.AddToDatabase();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        };
    }
    
    public void CreateOnNewDataReadyEvent(FlightsGUIDataAdapter flightsGuiDataAdapter, UserCommands userCommands)
    {
        NetSimulator.OnNewDataReady += (sender, args) =>
        {
            try
            {
                var message = NetSimulator.GetMessageAt(args.MessageIndex);
                var obj = ObjFactory.Create(message);
                var key = ObjFactory.GetKey(message);
                obj.AddToDatabase();
                if (key == Flight.fileId)
                    flightsGuiDataAdapter.AddFlightGUIAdapter((Flight)obj);
                try
                {
                    IReportable reportable = Database.Instance.FindReportable(obj.Id);
                    userCommands.AddReportable(reportable);
                }
                catch (Exception e)
                {
                    if (e is not KeyNotFoundException)
                        throw;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        };
    }

    // Creates a handler for IDUpdate event
    public void CreateOnIDUpdateEvent(string logsPath)
    {
        NetSimulator.OnIDUpdate += (sender, args) =>
        {
            try
            {
                Database.Instance.ChangeId(args.ObjectID, args.NewObjectID);
                AppendToLog(logsPath, $"{DateTime.Now} - AirportObject ID changed\n" +
                                 $"\t ID: {args.ObjectID} -> {args.NewObjectID}");
            }
            catch (Exception e)
            {
                if (e is not KeyNotFoundException)
                    throw;
                
                AppendToLog(logsPath, $"{DateTime.Now} - {e.Message}");
            }
        };
    }
    
    // Creates a handler for PositionUpdate event
    public void CreateOnPositionUpdateEvent(string logsPath)
    {
        NetSimulator.OnPositionUpdate += (sender, args) =>
        {
            try
            {
                try
                {
                    var obj = Database.Instance.FindFlight(args.ObjectID);
                    obj.Update(args, logsPath);
                }
                catch (Exception e)
                {
                    if (e is not KeyNotFoundException)
                        throw;
                    var obj = Database.Instance.FindAirplane(args.ObjectID);
                    obj.Notify(args, logsPath);
                }
            }
            catch (Exception e)
            {
                if (e is not KeyNotFoundException)
                    throw;
                AppendToLog(logsPath, $"{DateTime.Now} - No object with id: {args.ObjectID} to be changed in database");
            }
        };
    }
    
    // Creates a handler for ContactInfoUpdate event
    public void CreateOnContactInfoUpdateEvent(string logsPath)
    {
        NetSimulator.OnContactInfoUpdate += (sender, args) =>
        {
            try
            {
                var obj = Database.Instance.FindPerson(args.ObjectID);
                AppendToLog(logsPath, $"{DateTime.Now} - Contact info for person with ID: {args.ObjectID} changed\n" +
                                      $"\t Email: {obj.GetEmail} -> {args.EmailAddress}\n" +
                                      $"\t Phone: {obj.GetPhone} -> {args.PhoneNumber}");
                obj.UpdateContactInfo(args.PhoneNumber, args.EmailAddress);
            }
            catch (Exception e)
            {
                if (e is not KeyNotFoundException)
                    throw;
                
                AppendToLog(logsPath, $"{DateTime.Now} - {e.Message}");
            }
        };
    }

    // Method writes given message to the end of the log
    public static void AppendToLog(string logsPath, string message)
    {
        using (StreamWriter writer = new StreamWriter(logsPath, true))
        {
            writer.WriteLine(message);
        }
    }

    // Method runs the NetSimulator.Run() command on a separate background thread, which ends as soon, as tha main
    // thread exits
    public void RunSimulator()
    {
        Thread netThread = new Thread(() => { NetSimulator.Run(); }) { IsBackground = true };
        netThread.Start();
    }
    
    // Method creates the right event handlers and then runs the NetSimulator.Run() command on a separate background
    // thread, which ends as soon, as tha main thread exits
    public void RunSimulator(string logsPath)
    {
        using (StreamWriter writer = new StreamWriter(logsPath))
        {
            writer.WriteLine($"{DateTime.Now} - Server started");
        }
        
        CreateOnIDUpdateEvent(logsPath);
        CreateOnPositionUpdateEvent(logsPath);
        CreateOnContactInfoUpdateEvent(logsPath);
        
        Thread netThread = new Thread(() => { NetSimulator.Run(); }) { IsBackground = true };
        netThread.Start();
    }
}