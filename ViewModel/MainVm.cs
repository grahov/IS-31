using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IS_31.Model;
using System.Windows;
using System.Data.Entity;

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
            using (var context = new CollegeEntities())
            {
                Students = await context.Student.ToListAsync();
            }
        }
    }
}