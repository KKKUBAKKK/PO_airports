using airports_PO.AirportObjects;
using airports_PO.Functionality;
using FlightTrackerGUI;

namespace airports_PO.GUI;

// ManagerGUI manages the animated map of flights. It runs different sections in different threads.
public class ManagerGUI
{
    public readonly FlightsGUIDataAdapter FlightsGui;
    private const int UpdateIntervalInMs = 10;

    public ManagerGUI()
    {
        FlightsGui = new FlightsGUIDataAdapter();
        FlightsGui.UpdateFlightsGUIData();
    }

    public void RunWindowThread()
    {
        Thread thread = new Thread(() => Runner.Run()) { IsBackground = true };
        thread.Start();
    }

    public void RunFlightManagerThread()
    {
        Thread thread = new Thread(RuntimeUpdatesGui) { IsBackground = true };
        thread.Start();
    }

    public void RuntimeUpdatesGui()
    {
        while (true)
        {
            FlightsGui.UpdateFlightsGUIData();
            TimerGUI.UpdateTime();
            Runner.UpdateGUI(FlightsGui);
            Thread.Sleep(UpdateIntervalInMs);
        }
    }
}