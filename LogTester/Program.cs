using Logging4Net;
using NLogging;
using Serilogger;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogTester
{
    class Program
    {
        private static int NumberOfTasks = 10;
        private static int NumbersToPrint = 100000;

        static void Main(string[] args)
        {
            //Tasks10NumbersToPrint100000();
            Tasks1NumbersToPrint1000000();
            Console.ReadKey();
        }

        private static void Tasks10NumbersToPrint100000()
        {
            NumberOfTasks = 10;
            NumbersToPrint = 100000;
            //Results
            //10.5457298 sec
            //Log4Net();

            //Waaaaaaaaaay too long don't know why
            // Log4NetAsync();

            //3.8623906 sec 
            //NLogAsync();

            //0.8083774 sec
            //NLogAsyncWithoutThread();

            //7.6416944 sec;
            //NLog();

            //2.5519989 sec
            //NLogBuffer();

            //3.4488564 sec
            //SeriLog();
        }


        private static void Tasks1NumbersToPrint1000000()
        {
            NumberOfTasks = 1;
            NumbersToPrint = 1000000;
            //Results
            //8.8000969 sec
            //Log4Net();

            //Waaaaaaaaaay too long don't know why
            //   Log4NetAsync();

            //3.3822419 sec 
            //NLogAsync();

            //0.8873853 sec
            //NLogAsyncWithoutThread();

            //6.0948013 sec;
            //NLog();

            //1.8379628 sec
            //NLogBuffer();

            //5.5271542 sec
            //SeriLog();
        }


        private static void SeriLog()
        {
            ThreadedLogging(SeriLogger.PrintText, "Normal SeriLog");
        }

        private static void Log4Net()
        {
            Log4NetTestLogger.SetNormalLogging();
            ThreadedLogging(Log4NetTestLogger.PrintText, "Normal Log4Net");
        }

        private static void Log4NetAsync()
        {
            Log4NetTestLogger.SetAsyncLogging();
            ThreadedLogging(Log4NetTestLogger.PrintText, "Async Log4Net");
        }

        private static void NLogAsync()
        {
            NLogTestLogger.SetLoggerToAsync();
            ThreadedLogging(NLogTestLogger.PrintText, "Async NLog");
        }

        private static void NLogAsyncWithoutThread()
        {
            NLogTestLogger.SetLoggerToAsyncWithoutThread();
            ThreadedLogging(NLogTestLogger.PrintText, "Async NLog");
        }

        private static void NLog()
        {
            NLogTestLogger.SetLoggerToNormal();
            ThreadedLogging(NLogTestLogger.PrintText, "Normal NLog");
        }

        private static void NLogBuffer()
        {
            NLogTestLogger.SetLoggerBuffer();
            ThreadedLogging(NLogTestLogger.PrintText, "Buffer NLog");
        }

        private static void ThreadedLogging(Action<string> loggingAction, string loggerName)
        {
            Task[] tasks = new Task[NumberOfTasks];
            var numbers = Enumerable.Range(0, NumbersToPrint).Select(x => x.ToString());
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < NumberOfTasks; i++)
            {
                Task t = new Task(() =>
                {
                    foreach (var number in numbers)
                    {
                        loggingAction(number);
                    }
                });
                tasks[i] = t;
                t.Start();
            }

            Task.WaitAll(tasks);
            stopwatch.Stop();
            Console.WriteLine($"{loggerName} - Done in: {stopwatch.Elapsed}");
        }
    }
}
