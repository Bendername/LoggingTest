using NLogging;
using System;
using System.Linq;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            NLogTestLogger testLogger = new NLogTestLogger();

            var numbers = Enumerable.Range(0, 100000);
            foreach(var number in numbers.Select(x=>x.ToString()))
            {
                testLogger.PrintText(number);
            }
        }
    }
}
