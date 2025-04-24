using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using IS_31.Model;
using IS_31.ViewModel;

namespace IS_31.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainVm();

            this.Activated += MainWindowActivated;
        }

        private void MainWindowActivated(object sender, EventArgs e)
        {
            (DataContext as MainVm).LoadStudents();
        }

        private void addStudentBtn_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainVm).OpenAddWindow();
        }

        private void deleteStudentBtn_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainVm).DeleteStudent((mainDg.SelectedItem as Student).Id);

        }
    }
}
