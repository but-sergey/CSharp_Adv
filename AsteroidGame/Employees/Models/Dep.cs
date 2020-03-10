using System.ComponentModel;

namespace Employees.Models
{
    public class Dep : INotifyPropertyChanged
    {
        private int _Id;
        private string _Name;
        private Empl _Manager;

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

        public Empl Manager 
        {
            get => _Manager;
            set
            {
                _Manager = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Manager)));
            }
        }

        public Dep()
        {
            Id = GID.GetDepId();
        }

        public override string ToString()
        {
            return $"{Name} (id: {Id})";
        }
    }
}
