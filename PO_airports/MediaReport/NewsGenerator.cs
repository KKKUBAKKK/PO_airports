using System.Collections;

namespace airports_PO.MediaReport;

// Class is an IEnumerable<string> that lets user enumerate threw all the news about all the objects
public class NewsGenerator : IEnumerable<string>
{
    public List<Media> MediaSources;
    public List<IReportable> Reportables;
    public IVisitor Visitor;

    public NewsGenerator(List<Media> mediaSources, List<IReportable> reportables, IVisitor visitor)
    {
        MediaSources = mediaSources;
        Reportables = reportables;
        Visitor = visitor;
    }

    public IEnumerator<string> GetEnumerator()
    {
        foreach (var mediaSource in MediaSources)
        {
            foreach (var reportable in Reportables)
            {
                yield return reportable.Accept(Visitor, mediaSource);
            }
        }
    }
    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerable<string> GenerateNextNews()
    {
        foreach (var mediaSource in MediaSources)
        {
            foreach (var reportable in Reportables)
            {
                yield return reportable.Accept(Visitor, mediaSource);
            }
        }
    }
}