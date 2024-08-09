namespace airports_PO.GUI;

// This class works as a timer. Without it the project would be even easier, but I had to add some way of simulating
// faster time flow in order for the planes to visibly move.
public class TimerGUI
{
    private static DateTime time = DateTime.Now;
    public const double TimeIntervalInMinutes = 1;
    
    public static DateTime Time { get { return time; } }

    public static void UpdateTime()
    {
        time = time.AddMinutes(TimeIntervalInMinutes);
    }
}