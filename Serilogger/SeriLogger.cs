using Serilog;

namespace Serilogger
{
    public class SeriLogger
    {
        public static ILogger log = new LoggerConfiguration().Enrich.WithThreadId().WriteTo.Async(a => a.File("serilog.txt", rollingInterval: RollingInterval.Day, buffered: true, rollOnFileSizeLimit: true, retainedFileCountLimit: 1)).CreateLogger();


        public static void PrintText(string s)
        {
            log.Information(s);
        }

    }
}
