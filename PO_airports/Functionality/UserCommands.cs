using airports_PO.AirportObjects;
using airports_PO.MediaReport;
using DynamicData;

namespace airports_PO.Functionality;

// Class that handles user commands. 
public class UserCommands
{
    public readonly ISerializer Serializer;
    public readonly NewsGenerator News;
    public readonly List<Media> MediaSources;
    private List<IReportable> Reportables;
    public readonly Object ReportablesLock;
    public IVisitor Visitor;

    public UserCommands(ISerializer serializer)
    {
        Serializer = serializer;
        
        MediaSources = new List<Media>();
        MediaSources.Add(new Television("Telewizja Abelowa"));
        MediaSources.Add(new Television("Kanal TV-tensor"));
        MediaSources.Add(new Radio("Radio Kwantyfikator"));
        MediaSources.Add(new Radio("Radio Shmem"));
        MediaSources.Add(new Newspaper("Gazeta Kategoryczna"));
        MediaSources.Add(new Newspaper("Dziennik Politechniczny"));

        Reportables = new List<IReportable>();
        ReportablesLock = new object();

        Visitor = new Visitor();

        News = new NewsGenerator(MediaSources, Reportables, Visitor);
    }

    public void AddReportable(IReportable obj)
    {
        lock (ReportablesLock)
        {
            Reportables.Add(obj);
        }
    }
    
     // Executing GetUserCommands in a different thread
    public void RunGetUserCommandsThread<T>(T data, string snapshotPath)
    {
        Thread thread = new Thread(() => GetUserCommands(data, snapshotPath)) { IsBackground = true };
        thread.Start();
    }
    
    // Executing GetUserCommands in a different thread
    public void RunGetUserCommandsThread(string snapshotPath)
    {
        Thread thread = new Thread(() => GetUserCommandsWithoutExit(Database.Instance, snapshotPath)) 
            { IsBackground = true };
        thread.Start();
    }

    // Method displays available commands and waits for input from user, then performs chosen actions
    // print - makes a snapshot to a file
    // exit - makes a last snapshot and then exits
    public void GetUserCommands<T>(T data, string snapshotPath)
    {
        Console.WriteLine("Type chosen command: \"print\", \"report\", \"exit\"");
        while(true)
        {
            string? command = Console.ReadLine();
            command = command?.ToLower();
            if (command == "print")
            {
               var s = MakeSnapshot(data, snapshotPath);
               Console.WriteLine($"Created snapshot: {s}");
            }
            else if (command == "exit")
            {
                var s = MakeSnapshot(data, snapshotPath, "exit");
                Console.WriteLine($"Created snapshot: {s}");
                Console.WriteLine("Program exiting");
                break;
            }
            else if (command == "report")
            {
                Report();
            }
            else
            {
                Console.WriteLine("Invalid command");
            }
        }
    }
    
    public void GetUserCommandsWithoutExit<T>(T data, string snapshotPath)
    {
        Console.WriteLine("Type chosen command: \"print\", \"report\"");
        while(true)
        {
            string? command = Console.ReadLine();
            command = command?.ToLower();
            if (command == "print")
            {
                var s = MakeSnapshot(data, snapshotPath);
                Console.WriteLine($"Created snapshot: {s}");
            }
            else if (command == "report")
            {
                Report();
            }
            else
            {
                Console.WriteLine("Invalid command");
            }
        }
    }

    // Method serializes data argument and saves it as a snapshot with timestamp, if name is provided it adds it after
    // the timestamp
    public string MakeSnapshot<T>(T data, string snapshotPath, string? postFix=null)
    {
        snapshotPath += "_" + DateTime.Now.ToString("HH_mm_ss");
        if (postFix != null)
            snapshotPath += '_' + postFix;
        lock(Database.DatabaseLock)
        {
            snapshotPath = Serializer.SaveToFile(Serializer.Serialize(data), snapshotPath);
        }
        return snapshotPath;
    }

    // Writes all the available news from the media to the console.
    public void Report()
    {
        lock (ReportablesLock)
        {
            foreach (var news in News)
            {
                Console.WriteLine(news);
            }
        }
    }
}