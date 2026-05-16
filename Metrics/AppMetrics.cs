using System.Diagnostics.Metrics;

namespace BackEnd_student.Metrics;

public class AppMetrics
{
    private readonly Counter<int> _requestCounter;
    private readonly Histogram<double> _requestDurationHistogram;
    private readonly ObservableGauge<int> _activeRequestsGauge;
    
    private int _activeRequests = 0;

    public AppMetrics(IMeterFactory meterFactory)
    {
        var meter = meterFactory.Create("BackEnd.Metrics");
        
        _requestCounter = meter.CreateCounter<int>("app.requests.total", 
            unit: "{count}",
            description: "Total number of requests processed");
        
        _requestDurationHistogram = meter.CreateHistogram<double>("app.requests.duration",
            unit: "ms",
            description: "Distribution of request processing times");
        
        _activeRequestsGauge = meter.CreateObservableGauge<int>("app.requests.active",
            observeValue: () => _activeRequests,
            unit: "{requests}",
            description: "Current number of active requests");
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