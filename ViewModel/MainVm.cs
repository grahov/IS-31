using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IS_31.Model;
using System.Windows;
using System.Data.Entity;
using IS_31.View;

namespace IS_31.ViewModel
{
    public class MainVm : BaseViewModel
    {
        private List<Student> _students;

        public List<Student> Students
        {
            get { return _students; }
            set
            {
                SetPropertyChanged(ref _students, value);
            }
        }

        public MainVm()
        {
            Students = new List<Student>();

            LoadStudents();
        }

        public async void LoadStudents()
        {
            Students.Clear();

            using (var context = new CollegeEntities())
            {
                Students = await context.Student.Include("Group").ToListAsync();
            }
        }

        public void OpenAddWindow()
        {
            var addWindow = new AddStudentWindow();
            addWindow.ShowDialog();
        }

        public void DeleteStudent(int studentId)
        {
            using (var context = new CollegeEntities())
            {
                var forDelete = context.Student.FirstOrDefault(student => student.Id == studentId); 
                context.Student.Remove(forDelete);

                context.SaveChanges();

                LoadStudents();
            }
        }
    }
}