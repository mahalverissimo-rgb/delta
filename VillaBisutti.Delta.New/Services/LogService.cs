using Microsoft.ApplicationInsights;
using Serilog;
using Serilog.Events;

namespace VillaBisutti.Delta.WebApp.Services
{
    public class LogService
    {
        private readonly Serilog.ILogger _logger;
        private readonly TelemetryClient _telemetryClient;

        public LogService(TelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient;

            _logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("logs/log-.txt", 
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 7)
                .WriteTo.ApplicationInsights(telemetryClient, TelemetryConverter.Traces)
                .CreateLogger();

            Log.Logger = _logger;
        }

        public void LogInformation(string message, params object[] args)
        {
            _logger.Information(message, args);
        }

        public void LogWarning(string message, params object[] args)
        {
            _logger.Warning(message, args);
        }

        public void LogError(Exception ex, string message, params object[] args)
        {
            _logger.Error(ex, message, args);
            TrackException(ex);
        }

        public void LogCritical(Exception ex, string message, params object[] args)
        {
            _logger.Fatal(ex, message, args);
            TrackException(ex);
        }

        public void TrackEvent(string eventName, IDictionary<string, string>? properties = null)
        {
            _telemetryClient.TrackEvent(eventName, properties);
        }

        public void TrackMetric(string name, double value)
        {
            _telemetryClient.TrackMetric(name, value);
        }

        private void TrackException(Exception ex)
        {
            _telemetryClient.TrackException(ex);
        }

    }
}
