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
        private Student _selectedStudent;

        public List<Student> Students
        {
            get { return _students; }
            set
            {
                SetPropertyChanged(ref _students, value);
            }
        }

        public Student SelectedStudent
        {
            get
            {
                return _selectedStudent;
            }

            set
            {
                SetPropertyChanged(ref _selectedStudent, value);
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
                Students = await context.Student.Include("Group").Where(student => student.IsDeleted == false).ToListAsync();
            }
        }

        public void OpenAddWindow(Student student)
        {
            var addWindow = new AddStudentWindow(student);
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