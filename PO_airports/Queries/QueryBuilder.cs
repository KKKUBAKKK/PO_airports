using airports_PO.Functionality;

namespace airports_PO.Queries;

public class QueryBuilder
{
    public static void BuildDisplayQuery(Parser.DispInfo info)
    {
        if (info.dispClass == "Flights")
        {
            var query = Database.Instance.Flights.Values.AsQueryable();
            
            
        }
        else if (info.dispClass == "Crews")
        {
            var query = Database.Instance.Crews.Values.AsQueryable();
        }
        else if (info.dispClass == "Passengers")
        {
            var query = Database.Instance.Passengers.Values.AsQueryable();
        }
        else if (info.dispClass == "Cargos")
        {
            var query = Database.Instance.Cargos.Values.AsQueryable();
        }
        else if (info.dispClass == "Airports")
        {
            var uery = Database.Instance.Airports.Values.AsQueryable();
        }
        else if (info.dispClass == "CargoPlanes")
        {
            var uery = Database.Instance.CargoPlanes.Values.AsQueryable();
        }
        else if (info.dispClass == "PassengerPlanes")
        {
            var uery = Database.Instance.PassengerPlanes.Values.AsQueryable();
        }
    }
}