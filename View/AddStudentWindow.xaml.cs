using System;
using System.Collections.Generic;
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
using IS_31.ViewModel;

namespace IS_31.View
{
    /// <summary>
    /// Логика взаимодействия для CourseWindow.xaml
    /// </summary>
    public partial class AddStudentWindow : Window
    {
        public AddStudentWindow()
        {
            InitializeComponent();

            DataContext = new AddStudentViewModel();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var result = (DataContext as AddStudentViewModel).AddStudent();

            if (result)
            {
                MessageBox.Show("Объект добавлен!");
                this.Close();
            }
        }
    }
}
