using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestConsole.Loggers;

namespace TestConsole
{
    static class Program
    {
        static void Main(string[] args)
        {
            //Logger logger = new ListLogger();
            //Logger logger = new FileLogger("program.log");
            //Logger logger = new VisualStudioOutputLogger();
            Logger logger = new TraceLogger();

            Trace.Listeners.Add(new TextWriterTraceListener("trace.logger"));

            logger.LogInformation("Start program");

            for(int i = 0; i < 10; i++)
                logger.LogInformation($"Do some work {i + 1}");

            logger.LogWarning("Application work out");

            //var log_messages = ((ListLogger)logger).Messages;

            Trace.Flush();

            Console.ReadLine();
        }
    }
}


