using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Targets.Wrappers;
using System.Text;

namespace NLogging
{
    public class NLogTestLogger
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static void SetLoggerToAsync()
        {
            FileTarget target = new FileTarget();
            target.FileName = "${basedir}/nlog/nlogAsync.txt";
            target.Encoding = Encoding.UTF8;
            target.ArchiveNumbering = ArchiveNumberingMode.Sequence;
            target.ConcurrentWrites = true;
            target.ArchiveOldFileOnStartup = true;
            target.MaxArchiveFiles = 3;
            target.KeepFileOpen = true;
            target.OptimizeBufferReuse = true;
           // target.ArchiveFileName = "${basedir}/archive/${LogDay}.{#######}.log";
            target.Layout = "${longdate}|${level:uppercase=true}|${threadid}|${logger}|${message}";

            AsyncTargetWrapper wrapper = new AsyncTargetWrapper();
            wrapper.WrappedTarget = target;
            wrapper.QueueLimit = 1000000;
            wrapper.BatchSize = 1000;
            wrapper.TimeToSleepBetweenBatches = 10;
            wrapper.OverflowAction = AsyncTargetWrapperOverflowAction.Grow;
            SimpleConfigurator.ConfigureForTargetLogging(wrapper, LogLevel.Debug);

        }

        public static void SetLoggerToAsyncWithoutThread()
        {
            FileTarget target = new FileTarget();
            target.FileName = "${basedir}/nlog/nlogAsyncWithoutThread.txt";
            target.Encoding = Encoding.UTF8;
            target.ArchiveNumbering = ArchiveNumberingMode.Sequence;
            target.ConcurrentWrites = true;
            target.ArchiveOldFileOnStartup = true;
            target.MaxArchiveFiles = 3;
            target.KeepFileOpen = true;
            target.OptimizeBufferReuse = true;
            // target.ArchiveFileName = "${basedir}/archive/${LogDay}.{#######}.log";
            target.Layout = "${longdate}|${level:uppercase=true}|${logger}|${message}";

            AsyncTargetWrapper wrapper = new AsyncTargetWrapper();
            wrapper.WrappedTarget = target;
            wrapper.QueueLimit = 1000000;
            wrapper.BatchSize = 1000;
            wrapper.TimeToSleepBetweenBatches = 10;
            wrapper.OverflowAction = AsyncTargetWrapperOverflowAction.Grow;
            SimpleConfigurator.ConfigureForTargetLogging(wrapper, LogLevel.Debug);

        }


        public static void SetLoggerToNormal()
        {
            FileTarget target = new FileTarget();
            target.FileName = "${basedir}/nlog/autoFlushLogger.txt";
            target.KeepFileOpen = true;
            target.Encoding = Encoding.UTF8;
            target.ArchiveOldFileOnStartup = true;

            AutoFlushTargetWrapper wrapper = new AutoFlushTargetWrapper();
            wrapper.WrappedTarget = target;
            wrapper.OptimizeBufferReuse = true;

            SimpleConfigurator.ConfigureForTargetLogging(wrapper, LogLevel.Debug);
        }

        public static void SetLoggerBuffer()
        {

            FileTarget target = new FileTarget();
            target.FileName = "${basedir}/nlog/BufferLogger.txt";
            target.KeepFileOpen = true;
            target.Encoding = Encoding.UTF8;

            BufferingTargetWrapper wrapper = new BufferingTargetWrapper();
            wrapper.WrappedTarget = target;
            wrapper.OptimizeBufferReuse = true;
            wrapper.FlushTimeout = 100;

            SimpleConfigurator.ConfigureForTargetLogging(wrapper, LogLevel.Debug);
        }


        public static void PrintText(string printText)
        {
            logger.Info(printText);
        }
    }
}
