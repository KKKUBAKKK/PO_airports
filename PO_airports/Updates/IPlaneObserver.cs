using NetworkSourceSimulator;

namespace airports_PO.Updates;

// TODO: dont know what to do, shoudl do observer and decorator, but no polymorphism, maybe just implement update in flight???
public interface IPlaneObserver
{
    public void Update(PositionUpdateArgs args);
}