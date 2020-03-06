﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students
{
    class Student : IComparable<Student>, IEquatable<Student>, IEquatable<string>, ICloneable<Student>
    {
        public string Name { get; set; }

        public List<int> Ratings { get; set; } = new List<int>();

        public double  AverageRating
        {
            get
            {
                //return Ratings.Average();
                return Ratings.Sum() / (double)Ratings.Count;
            }
        }

        //public double AverageRating
        //{
        //    get
        //    {
        //        var ratings = Ratings;
        //        if (ratings == null)
        //            throw new InvalidOperationException("Невозможно рассчитать среднюю оценку. Список оценок не задан");
        //        if (ratings.Count == 0)
        //            return double.NaN;

        //        var sum = 0d;
        //        for (var i = 0; i < ratings.Count; i++)
        //            sum += ratings[i];
        //        return sum / (double) ratings.Count;
        //    }
        //}

        public object Clone()
        {
            return new Student
            {
                Name = Name,
                Ratings = new List<int>(Ratings)
            };
        }

        public Student CloneObject()
        {
            return (Student)Clone();
        }

        public int CompareTo(Student other)
        {
            var current_average_rating = AverageRating;
            var other_average_rating = other.AverageRating;

            if (Math.Abs(current_average_rating - other_average_rating) < 0.001)
                return 0;
            if (current_average_rating > other_average_rating)
                return +1;
            else
                return -1;

        }

        public bool Equals(Student other)
        {
            return other?.Name == Name;
        }

        public bool Equals(string other)
        {
            return Name == other;
        }

        //public int CompareTo(object obj)
        //{
        //    var other_student = (Student)obj;
        //    var current_average_rating = AverageRating;
        //    var other_average_rating = other_student.AverageRating;

        //    if (Math.Abs(current_average_rating - other_average_rating) < 0.001)
        //        return 0;
        //    if (current_average_rating > other_average_rating)
        //        return +1;
        //    else
        //        return -1;
        //}

        public override string ToString() => $"{Name} : {AverageRating:0.##}";

    }
}
