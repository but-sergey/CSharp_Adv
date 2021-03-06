﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace Students
{
    abstract class Storage<TItem> : IEnumerable<TItem>
    {
        public event Action<TItem> NewItemAdded;

        protected readonly List<TItem> _Items = new List<TItem>();
        protected Action<TItem> _AddObservers;
        protected Action<TItem> _RemoveObservers;

        public void Add(TItem Item)
        {
            if (_Items.Contains(Item)) return;
            _Items.Add(Item);

            _AddObservers?.Invoke(Item);

            NewItemAdded?.Invoke(Item);
        }

        public bool Remove(TItem Item)
        {
            var result = _Items.Remove(Item);
            if (result)
                _RemoveObservers?.Invoke(Item);
            return result;
        }

        public void Clear()
        {
            _Items.Clear();
        }

        public abstract void SaveToFile(string FileName);

        public virtual void LoadFromFile(string FileName)
        {
            Clear();
        }

        public IEnumerator<TItem> GetEnumerator()
        {
            for (var i = 0; i < _Items.Count; i++)
                yield return _Items[i];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void SubscribeToAdd(Action<TItem> Observer)
        {
            _AddObservers += Observer;
        }

        public void SubscribeToRemove(Action<TItem> Observer)
        {
            _RemoveObservers += Observer;
        }
    }

    class Dekanat : Storage<Student>
    {
        public event Action<Student> ExelentStudentAdded;

        public Dekanat()
        {
            NewItemAdded += OnNewItemAdded;
        }

        private void OnNewItemAdded(Student student)
        {
            if (student.AverageRating > 4.5)
                ExelentStudentAdded?.Invoke(student);
        }

        public override void SaveToFile(string FileName)
        {
            using(var file_writer = File.CreateText(FileName))
            {
                foreach (var student in _Items)
                {
                    file_writer.Write(student.Name);
                    if(student.Ratings.Count > 0)
                    {
                        var ratings_string = string.Join(",", student.Ratings);
                        file_writer.Write($",{ratings_string}");
                    }

                    file_writer.WriteLine();
                }
            }
        }

        public override void LoadFromFile(string FileName)
        {
            //if (!File.Exists(FileName)) return;
            if (!File.Exists(FileName))
                throw new FileNotFoundException("Файл с данными деканата не найден", FileName);
            base.LoadFromFile(FileName);

            using (var file_reader = File.OpenText(FileName))
            {
                while(!file_reader.EndOfStream)
                {
                    var str = file_reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(str)) continue;

                    var data_elements = str.Split(',');
                    if (data_elements.Length == 0) continue; ;

                    var student = new Student { Name = data_elements[0] };
                    if(data_elements.Length > 1)
                    {
                        var ratings = new List<int>();
                        for (var i = 1; i < data_elements.Length; i++)
                            ratings.Add(int.Parse(data_elements[i]));
                        student.Ratings = ratings;
                    }

                    Add(student);
                }
            }
        }
    }
}
