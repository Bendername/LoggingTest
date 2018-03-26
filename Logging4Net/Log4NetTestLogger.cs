using Easy.Logger;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using Log4Net.Async;

namespace Logging4Net
{
    public class Log4NetTestLogger
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Log4NetTestLogger));


        public static void SetNormalLogging()
        {
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();

            PatternLayout patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = "%date [%thread] %-5level %logger - %message%newline";
            patternLayout.ActivateOptions();

            RollingFileAppender roller = new RollingFileAppender();
            roller.AppendToFile = false;
            roller.File = @"log4net\log.txt";
            roller.Layout = patternLayout;
            roller.MaxSizeRollBackups = 5;
            roller.MaximumFileSize = "1000MB";
            roller.RollingStyle = RollingFileAppender.RollingMode.Composite;
            roller.StaticLogFileName = true;
            roller.ActivateOptions();
            hierarchy.Root.AddAppender(roller);

            hierarchy.Root.Level = Level.All;
            hierarchy.Configured = true;
        }

        public static void SetAsyncLogging()
        {
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();

            PatternLayout patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = @"%date{ISO8601} [%-5level] [%2thread] %logger{1} - %message%newline%exception";
            patternLayout.ActivateOptions();

            RollingFileAppender roller = new RollingFileAppender();
            roller.AppendToFile = false;
            roller.File = @"log4net\log.txt";
            roller.Layout = patternLayout;
            roller.MaxSizeRollBackups = 5;
            roller.MaximumFileSize = "1000MB";
            roller.RollingStyle = RollingFileAppender.RollingMode.Composite;
            roller.StaticLogFileName = true;
            roller.ActivateOptions();

            AsyncBufferingForwardingAppender asyncBufferingForwarder = new AsyncBufferingForwardingAppender();
            asyncBufferingForwarder.AddAppender(roller);
            asyncBufferingForwarder.BufferSize = 512;
            asyncBufferingForwarder.Lossy = false;



            hierarchy.Root.AddAppender(asyncBufferingForwarder);
            hierarchy.Root.Level = Level.All;
            hierarchy.Configured = true;
        }
        
        public static void PrintText(string printText)
        {
            log.Info(printText);
        }
    }
}
