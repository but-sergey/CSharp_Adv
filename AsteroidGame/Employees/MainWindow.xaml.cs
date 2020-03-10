using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Employees.Models;
using Employees.Data;

namespace Employees
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Создаем нового сотрудника
            Empl Emp = new Empl();
            TestData.Emps.Add(Emp);

            // Вызываем окно редактирования его данных
            Employee EmpWindow = new Employee();
            EmpWindow.cmbDep.ItemsSource = TestData.Deps;
            EmpWindow.Emp = Emp;
            EmpWindow.ShowDialog();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // удаление выделенного сотрудника
            TestData.Emps.Remove((Empl)lbEmployee.SelectedItem);
        }

        private void lbEmployee_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Employee EmpWindow = new Employee();
            EmpWindow.cmbDep.ItemsSource = TestData.Deps;
            EmpWindow.Emp = (Empl)lbEmployee.SelectedItem;
            EmpWindow.ShowDialog();
        }

        private void btnDep_Click(object sender, RoutedEventArgs e)
        {
            Departments DepWindow = new Departments();
            DepWindow.lbDepartments.ItemsSource = TestData.Deps;
            DepWindow.ShowDialog();
        }
    }
}
