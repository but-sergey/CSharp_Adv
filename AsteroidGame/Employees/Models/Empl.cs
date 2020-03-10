using System.ComponentModel;

namespace Employees.Models
{
    public class Empl : INotifyPropertyChanged
    {
        private int _Id;
        private string _Name;
        private int _Age;
        private double _Salary;
        private Dep _Dep;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Id
        { 
            get => _Id;
            private set
            {
                _Id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
            }
        }

        public string Name
        { 
            get => _Name;
            set
            {
                _Name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        public int Age
        {
            get => _Age;
            set
            {
                _Age = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Age)));
            }
        }

        public double Salary 
        {
            get => _Salary;
            set
            {
                _Salary = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Salary)));
            }
        }

        public Dep Dep 
        { 
            get => _Dep;
            set
            {
                _Dep = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Dep)));
            } 
        }


        public Empl()
        {
            Id = GID.GetEmpId();
        }

        public override string ToString()
        {
            string IsManager = (Id == Dep?.Manager?.Id) ? " (начальник)" : "";
            return $"{Id}\t{Name}\t{Age}\t{Salary}\t{Dep?.Name}{IsManager}";
        }
    }
}
