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
using System.Windows.Shapes;
using Employees.Data;
using Employees.Models;

namespace Employees
{
    /// <summary>
    /// Interaction logic for Departments.xaml
    /// </summary>
    public partial class Departments : Window
    {
        public Departments()
        {
            InitializeComponent();
        }

        private void lbDepartments_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Department DepWindow = new Department();
            DepWindow.Dep = (Dep)lbDepartments.SelectedItem;
            DepWindow.ShowDialog();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Создаем новый отдел
            Dep dep = new Dep();
            TestData.Deps.Add(dep);

            // Вызываем окно редактирования его данных
            Department DepWindow = new Department();
            DepWindow.Dep = dep;
            DepWindow.ShowDialog();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // удаление выделенного отдела
            TestData.Deps.Remove((Dep)lbDepartments.SelectedItem);
        }
    }
}
