using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Students.Extensions;

namespace Students
{
    class Program
    {
        private static List<int> GetRandomRatings(Random rnd, int CountMin, int CountMax)
        {
            var count = rnd.Next(CountMin, CountMax + 1);
            var result = new List<int>(count);

            for (var i = 0; i < count; i++)
                result.Add(rnd.Next(2, 6));

            return result;
        }


        static void Main(string[] args)
        {
            var dekanat = new Dekanat();
            var rnd = new Random();

            for (var i = 0; i < 100; i++)
                dekanat.Add(new Student
                {
                    Name = $"Student {i + 1}",
                    Ratings = rnd.GetRandomIntValues(20, 2, 6).ToList()
                });

            const string students_data_file = "students.csv";
            dekanat.SaveToFile(students_data_file);

            var dekanat2 = new Dekanat();
            dekanat2.LoadFromFile(students_data_file);

            var student = new Student
            {
                Name = $"Student",
                Ratings = GetRandomRatings(rnd, 20, 50)
            };

            //var result = student.CompareTo(dekanat);

            //foreach (var std in dekanat2)
            //{
            //    Console.WriteLine(std);
            //}

            var average_rating = dekanat2.Average(s => s.AverageRating);
            var sum_average_rating = dekanat2.Sum(s => s.AverageRating);

            var random_student_name = rnd.NextValue("Иванов", "Петров", "Сидоров");

            var random_rating = rnd.NextValue(2, 3, 4, 5);

            //StudentProcessor processor = new StudentProcessor(GetIndexedStudentName);
            //StudentProcessor processor = GetIndexedStudentName;

            //var index = 0;
            //foreach (var s in dekanat2)
            //    Console.WriteLine(processor(s, index++));

            //Console.ReadLine();
            //processor = GetAverageStudentRating;
            //index = 0;
            //foreach (var s in dekanat2)
            //    Console.WriteLine(processor(s, index++));

            //Console.ReadLine();
            //ProcessStudents(dekanat2, GetIndexedStudentName);

            //Console.ReadLine();
            //ProcessStudents(dekanat2, GetAverageStudentRating);

            //ProcessStudentStandard(dekanat2, PrintStudent);

            //var metric = GetStudentMetrics(dekanat2, std => std.Name.Length + (int)(student.AverageRating * 10));

            Console.ReadLine();
        }

        private static void PrintStudent(Student student)
        {
            Console.WriteLine("Студент: {0}", student.Name);
        }

        public static void ProcessStudentStandard(IEnumerable<Student> Students, Action<Student> action)
        {
            foreach (var s in Students)
            {
                action(s);
            }
        }

        public static int[] GetStudentMetrics(IEnumerable<Student> Students, Func<Student, int> GetMetric)
        {
            var result = new List<int>();

            foreach (var student in Students)
            {
                result.Add(GetMetric(student));
            }

            return result.ToArray();
        }

        public static void ProcessStudents(IEnumerable<Student> Students, StudentProcessor Processor)
        {
            var index = 0;
            foreach (var s in Students)
                Console.WriteLine(Processor(s, index + 1));
        }

        private static string GetIndexedStudentName(Student student, int Index)
        {
            return $"{student.Name}[{Index}]";
        }

        public static string GetAverageStudentRating(Student student, int Index)
        {
            return $"[{Index}]:{student.Name} - {student.AverageRating}";
        }


    }

    internal delegate string StudentProcessor(Student Student, int Index);
}
