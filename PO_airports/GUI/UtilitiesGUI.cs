using System.Reactive.PlatformServices;
using airports_PO.AirportObjects;
using airports_PO.Functionality;
using Mapsui.Projections;

namespace airports_PO.GUI;

// Utilities used while using GUI
public static class UtilitiesGUI
{
    public static double CalculateAngle((float latitude, float longitude) op, (float latitude, float longitude) tp)
    {
        (double x, double y) o = SphericalMercator.FromLonLat(op.longitude, op.latitude);
        (double x, double y) t = SphericalMercator.FromLonLat(tp.longitude, tp.latitude);

        if (t.x > o.x)
        {
            double angle = Math.Atan2(t.y - o.y, t.x - o.x);
            if (angle < Math.PI / 2.0)
                return Math.PI / 2.0 - angle;

            return 3.0 * Math.PI / 2.0 - angle;
        }
        else
        {
            double angle = Math.Atan2(o.y - t.y, o.x - t.x);
            if (angle < Math.PI / 2.0)
                return 3.0 * Math.PI / 2.0 - angle;

            return 5.0 * Math.PI / 2.0 - angle;
        }
    }

    public static (float latitude, float longitude) GetOriginPosition(Flight flight)
    {
        return (flight.Origin.Latitude, flight.Origin.Longitude);
    }

    public static (float latitude, float longitude) GetTargetPosition(Flight flight)
    {
        return (flight.Target.Latitude, flight.Target.Longitude);
    }

    public static DateTime TurnToDate(string date)
    {
        return DateTime.ParseExact(date, "HH:mm", null);
    }
}