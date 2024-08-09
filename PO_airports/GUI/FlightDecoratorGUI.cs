using airports_PO.AirportObjects;
using SkiaSharp;

namespace airports_PO.GUI;

// The class Flight which I created at the start of the project following the instructions lacked some useful
// functionalities, so I created FlightDecoratorGUI. This class just adds some more functionality to Flight class, 
// which is useful when using the animated GUI. It is derived from FlightGUI to make it work with provided map.
public class FlightDecoratorGUI : FlightGUI
{
    private Flight _flight;

    public float Latitude { get { return _flight.Latitude; } }
    public float Longitude { get { return _flight.Longitude; } }
    public UInt64 Id { get { return _flight.Id; } }
    public double Rotation { get { return _rotation; } }
    
    private (float latitude, float longitude) _originPosition;
    private (float latitude, float longitude) _targetPosition;

    private double _rotation;

    private float _flightTimeInS;
    private DateTime _takeoffTime;
    private DateTime _landingTime;

    public FlightDecoratorGUI(Flight flight)
    {
        _flight = flight;
        
        _originPosition = UtilitiesGUI.GetOriginPosition(_flight);
        _targetPosition = UtilitiesGUI.GetTargetPosition(_flight);
        _flight.Longitude = _originPosition.longitude;
        _flight.Latitude = _originPosition.latitude;

        _rotation = UtilitiesGUI.CalculateAngle(_originPosition, _targetPosition);
        
        _takeoffTime = UtilitiesGUI.TurnToDate(flight.TakeoffTime);
        _landingTime = UtilitiesGUI.TurnToDate(flight.LandingTime);
        if (_landingTime < _takeoffTime)
            _landingTime = _landingTime.AddDays(1);
        while (_takeoffTime < TimerGUI.Time)
        {
            _takeoffTime = _takeoffTime.AddDays(1);
            _landingTime = _landingTime.AddDays(1);
        }

        _flightTimeInS = (float) (_landingTime - _takeoffTime).TotalSeconds;
    }

    public void UpdatePosition()
    {
        float toBeFlown = (float) (_landingTime - TimerGUI.Time).TotalSeconds;
        float steps = (float) toBeFlown / ((float)(60 * TimerGUI.TimeIntervalInMinutes));
        if (toBeFlown > (_landingTime - _takeoffTime).TotalSeconds)
            return;

        if ((TimerGUI.Time - _landingTime).TotalSeconds > 0)
        {
            _flight.Longitude = _targetPosition.longitude;
            _flight.Latitude = _targetPosition.latitude;
            return;
        }

        if (steps <= 1)
        {
            _rotation = UtilitiesGUI.CalculateAngle((_flight.Latitude, _flight.Longitude), _targetPosition);
            _flight.Longitude = _targetPosition.longitude;
            _flight.Latitude = _targetPosition.latitude;
            return;
        }

        float dx = _targetPosition.latitude - _flight.Latitude;
        float dy = _targetPosition.longitude - _flight.Longitude;
        _flight.Latitude = _flight.Latitude + dx / steps;
        _flight.Longitude = _flight.Longitude + dy / steps;
        _rotation = UtilitiesGUI.CalculateAngle((_flight.Latitude, _flight.Longitude), _targetPosition);
    }

    public bool IsReady()
    {
        float flown = (float) (TimerGUI.Time - _takeoffTime).TotalSeconds;
        
        if (flown < 0 || flown > TimerGUI.TimeIntervalInMinutes * 60)
            return false;
        
        return true;
    }

    public bool IsComplete()
    {
        float flown = (float) (TimerGUI.Time - _takeoffTime).TotalSeconds;
        if (flown < 0)
            return false;
        
        float coef = flown / _flightTimeInS;
        if (coef >= 1)
            return true;
        
        return false;
    }
}