namespace airports_PO.Queries;

// Parser scans terminal for input and parses the commands provided by user
public class Parser
{
    public static readonly List<string> Commands = new List<string>() { "display", "update", "delete", "add" };
    

    public static readonly List<string> Classes = new List<string>()
        { "Crew", "Passenger", "Cargo", "CargoPlane", "PassengerPlane", "Airport", "Flight" };
    
    public struct DispInfo
    {
        public string dispClass;
        public string[] objFields;
        public string[] conditions;
    }
    
    
    public static string[] CleanAndSplit(string query)
    {
        var q = query.Split(' ');
        for (int i = 0; i < q.Length; i++)
        {
            q[i] = q[i].Trim(' ', ',', '.', '(', ')');
        }

        return q;
    }

    // Returns index in Commands
    public static int ParseCommand(string[] q)
    {
        return Commands.FindIndex((s => q[0] == s));
    }

    // Returns index in q
    public static int ParseSecCommand(string[] q, string c)
    {
        if (c == "display")
            return FindInQuery(q, "from");
        if (c == "update")
            return FindInQuery(q, "set");
        if (c == "add")
            return FindInQuery(q, "new");
        return -1;
    }

    public static int FindInQuery(string[] q, string s)
    {
        for (int i = 0; i < q.Length; i++)
        {
            if (q[i] == s)
                return i;
        }
        return -1;
    }

    public static string[] ParseObjectFields(string[] q)
    {
        var l = new List<string>();
        for (int i = 1; i < FindInQuery(q, "from"); i++)
        {
            l.Add(q[i]);
        }

        return l.ToArray();
    }

    public static DispInfo ParseDisplay(string[] q, string l)
    {
        DispInfo info;
        info.objFields = ParseObjectFields(q);
        
        info.dispClass = q[info.objFields.Length + 2];

        int i = FindInQuery(q, "where");
        List<string> conditions = new List<string>();
        for (int j = i + 1; j < q.Length; j++)
            conditions.Add(q[j]);
        info.conditions = conditions.ToArray();
        
        return info;
    }
}