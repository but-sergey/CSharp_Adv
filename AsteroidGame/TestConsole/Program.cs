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
            //TraceLogger trace_logger = null;
            //try
            //{
            //    trace_logger = new TraceLogger();
            //    trace_logger.Log("123");
            //}
            //finally
            //{
            //    trace_logger.Dispose();
            //}

            using (var trace_logger = new TraceLogger())
                trace_logger.Log("123");


            //Logger logger = new ListLogger();
            //Logger logger = new FileLogger("program.log");
            //Logger logger = new VisualStudioOutputLogger();
            Logger logger = new TraceLogger();

            Trace.Listeners.Add(new TextWriterTraceListener("trace.logger"));

            var critical_logger = new ListLogger();
            var student_logger = new Student { Name = "Ivanov" };
            var student_clone = (Student)student_logger.Clone();

            ((ILogger)student_logger).LogError("Some error");

            DoSomeCriticalWork(student_logger);

            logger.LogInformation("Start program");

            for(int i = 0; i < 10; i++)
                logger.LogInformation($"Do some work {i + 1}");

            logger.LogWarning("Application work out");

            //var log_messages = ((ListLogger)logger).Messages;

            var random = new Random();
            var students = new Student[100];
            for (int i = 0; i < students.Length; i++)
                students[i] = new Student { Name = $"Student {i + 1}", Height = random.Next(150, 211) };

            Array.Sort(students);

            Trace.Flush();

            Console.ReadLine();
        }
     
        public static void DoSomeCriticalWork(ILogger log)
        {
            for(int i = 0; i < 10; i++)
            {
                log.LogInformation($"Do some very important work {i + 1}");
            }
        }
    }

    public class Student : ILogger, IComparable, ICloneable
    {
        private List<string> _Messages = new List<string>();

        public double Height { get; set; } = 175;

        public string Name { get; set; }

        public List<int> Ratings { get; set; } = new List<int>();

        public void Log(string Message)
        {
            Ratings.Add(Message.Length);
            _Messages.Add(Message);
        }

        //public void LogError(string Message)
        void ILogger.LogError(string Message)
        {
            Log("Error: " + Message);
        }

        public void LogInformation(string Message)
        {
            Log("Info: " + Message);
        }

        public void LogWarning(string Message)
        {
            Log("Warning: " + Message);
        }

        int IComparable.CompareTo(object obj)
        {
            if(obj is Student)
            {
                var other_Student = (Student)obj;
                //return StringComparer.OrdinalIgnoreCase.Compare(Name, other_Student.Name);
                if (Height > other_Student.Height)
                    return +1;
                else if (Height == other_Student.Height)
                    return 0;
                else
                    return -1;
            }
            if (obj is null)
                throw new ArgumentNullException("Попытка сравнения студента с пустотой");
            throw new ArgumentException("Попытка сравнения студента с " + obj.GetType().Name, nameof(obj));
        }

        public override string ToString() => $"{Name} - {Height}";

        public object Clone()
        {
            //var new_student = new Student
            //{
            //    Height = Height,
            //    Name = Name,
            //    Ratings = new List<int>(Ratings),
            //    _Messages = new List<string>(_Messages)
            //};

            var new_student = (Student)MemberwiseClone();
            new_student._Messages = new List<string>(_Messages);
            new_student.Ratings = new List<int>(Ratings);

            return new_student;
        }
    }
}


