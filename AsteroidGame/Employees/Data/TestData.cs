using Employees.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.Data
{
    static class TestData
    {
        public static ObservableCollection<Empl> Emps = new ObservableCollection<Empl>();
        public static ObservableCollection<Dep> Deps = new ObservableCollection<Dep>();

        static TestData()
        {
            // Заполняем департаменты
            Deps.Add(new Dep() { Name = "Сисадмины", Manager = null });
            Deps.Add(new Dep() { Name = "Разработчики", Manager = null });
            Deps.Add(new Dep() { Name = "Архитекторы", Manager = null });

            // Ищем департамент "Programmers" для добавления в него сотрудников
            Dep ProgDep = null;
            foreach (var dep in Deps)
                if (dep.Name == "Разработчики")
                {
                    ProgDep = dep;
                    break;
                }

            // добавляем сотрудников
            // менеджер
            var ProgManager = new Empl() { Name = "Василий", Age = 22, Salary = 3000, Dep = ProgDep };
            Emps.Add(ProgManager);
            // еще сотрудники
            Emps.Add(new Empl() { Name = "Петр", Age = 25, Salary = 6000, Dep = ProgDep });
            Emps.Add(new Empl() { Name = "Николай", Age = 23, Salary = 8000, Dep = ProgDep });

            // назначаем менеджера департаменту
            ProgDep.Manager = ProgManager;

            // связываем элемент ListBox с коллекцией сотрудников
            //lbEmployee.ItemsSource = Emps;

        }
    }
}
