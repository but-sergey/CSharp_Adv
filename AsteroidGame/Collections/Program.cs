using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Students;

namespace Collections
{
    class Program
    {
        static void Main(string[] args)
        {
            //var simple_array_list = new ArrayList();

            //simple_array_list.Add(52);
            //simple_array_list.Add(new object());
            //simple_array_list.Add(3.1415926);
            //simple_array_list.Add("Hello, world!");

            //for (var i = 0; i < simple_array_list.Count; i++)
            //{
            //    var value = simple_array_list[i];
            //    if (value is int)
            //    {
            //        int v = (int)value;
            //        Console.WriteLine("Int:{0}", v);
            //    }
            //    else if (value is string)
            //    {
            //        string v = (string)value;
            //        Console.WriteLine("String:{0}", v);
            //    }
            //    else if (value is double)
            //    {
            //        double v = (double)value;
            //        Console.WriteLine("Double:{0}", v);
            //    }
            //}

            List<Student> students = new List<Student>(45);

            for (var i = 0; i < 46; i++)
                students.Add(new Student());

            //students.Capacity = 10;

            var students_to_add = new Student[20];
            for (var i = 0; i < students_to_add.Length; i++)
                students_to_add[i] = new Student();

            students.AddRange(students_to_add);

            students.Capacity = students.Count;

            var new_students_list = new List<Student>(students_to_add);

            students.RemoveAt(5);

            var numbers_list = new List<int>(1000);
            for (var i = 0; i < numbers_list.Count; i++)
                numbers_list.Add(i);
            var value_index = numbers_list.BinarySearch(712);

            var string_list = new List<string>(1000);
            for (var i = 0; i < string_list.Capacity; i++)
                string_list.Add($"Message {i + 21}");

            //string_list.Sort((s1, s2) => StringComparer.Ordinal.Compare(s2, s1));

            //var string_array = string_list.ToArray();
            var string_array = new string[string_list.Count];
            string_list.CopyTo(string_array, 0);

            var str_value_index = string_list.BinarySearch("Message 712");

            string_list.ForEach(PrintString);


            Console.ReadLine();
        }

        private static void PrintString(string str) => Console.WriteLine("Str = " + str);
    }
}
