namespace airports_PO.MediaReport;

// IReportable interface is implemented by classes that can be reported by Media.
public interface IReportable
{
    public string Accept(IVisitor visitor, Media media);
}