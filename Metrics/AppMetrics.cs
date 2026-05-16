using System.Diagnostics.Metrics;

namespace BackEnd_student.Metrics;

public class AppMetrics
{
    private readonly Counter<int> _requestCounter;
    private readonly ObservableGauge<int> _activeRequestsGauge;
    private readonly Histogram<double> _requestDurationHistogram;
    
    private int _activeRequests = 0;

    public AppMetrics(IMeterFactory meterFactory)
    {
        var meter = meterFactory.Create("BackEnd.Metrics");
        
        _requestCounter = meter.CreateCounter<int>("app.requests.total", 
            description: "Total number of requests processed");
        
        _activeRequestsGauge = meter.CreateObservableGauge<int>("app.requests.active",
            () => _activeRequests,
            description: "Current number of active requests");
        
        _requestDurationHistogram = meter.CreateHistogram<double>("app.requests.duration",
            unit: "ms",
            description: "Distribution of request processing times");
    }

    public void RecordRequest()
    {
        _requestCounter.Add(1);
    }

    public void IncrementActiveRequests()
    {
        Interlocked.Increment(ref _activeRequests);
    }

    public void DecrementActiveRequests()
    {
        Interlocked.Decrement(ref _activeRequests);
    }

    public void RecordRequestDuration(double milliseconds)
    {
        _requestDurationHistogram.Record(milliseconds);
    }
}