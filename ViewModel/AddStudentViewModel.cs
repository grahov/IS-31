using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using IS_31.Model;

namespace IS_31.ViewModel
{
    public class AddStudentViewModel : BaseViewModel
    {
        private List<Group> _groups;
        private Group _selectedGroup;
        private Student _newStudent;

        public List<Group> Groups
        {
            get { return _groups; }
            set
            {
                SetPropertyChanged(ref _groups, value);
            }
        }

        public Student NewStudent
        {
            get { return _newStudent; }
            set
            {
                SetPropertyChanged(ref _newStudent, value);
            }
        }

        public Group SelectedGroup
        {
           get { return _selectedGroup; }
            set
            {
                SetPropertyChanged(ref _selectedGroup, value);
            }
        }

        public AddStudentViewModel()
        {
            _groups = new List<Group>();
            _newStudent = new Student();

            LoadGroup();
        }

        public void LoadGroup()
        {
            using(var context = new CollegeEntities())
            {
                Groups = context.Group.ToList();
            }
        }

        public bool AddStudent()
        {
            try
            {
                using (var context = new CollegeEntities())
                {
                    NewStudent.Group = new List<Group>();

                    var groupFromContext = context.Group.FirstOrDefault(group => group.Id == SelectedGroup.Id);

                    NewStudent.Group.Add(groupFromContext);
                    context.Student.Add(NewStudent);
                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
