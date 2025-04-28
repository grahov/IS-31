using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
        private string _errors;
        private Visibility _isError;

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

        public string Errors
        {
            get { return _errors; }
            set
            {
                SetPropertyChanged(ref _errors, value);
            }
        }

        public Visibility IsError
        {
            get { return _isError; }
            set
            {
                SetPropertyChanged(ref _isError, value);
            }
        }

        public AddStudentViewModel(Student student)
        {

            if (student == null)
            {
                _newStudent = new Student();
            }
            else
            {
                _newStudent = student;
            }

            _groups = new List<Group>();

            _isError = Visibility.Collapsed;

            LoadGroup();
        }

        public void LoadGroup()
        {
            using (var context = new CollegeEntities())
            {
                Groups = context.Group.ToList();
            }
        }

        private List<String> CheckFields()
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(NewStudent.Name))
            {
                errors.Add("Имя не может быть пустым");

                if (SelectedGroup.Name.Length < 2)
                {
                    errors.Add("Имя должно быть больше двух символов");
                }
            }

            if (NewStudent.Course < 1 && NewStudent.Course > 5)
            {
                errors.Add("Некорректный курс");
            }

            if (NewStudent.Age == null)
            {
                errors.Add("Укажите возраст");
            }

            if (NewStudent.Age < 14 && NewStudent.Age > 100)
            {
                errors.Add("Некорректный возраст");
            }

            return errors;
        }

        public bool AddOrUpdateStudent()
        {

            var result = CheckFields();

            if (result.Count > 0)
            {
                IsError = Visibility.Visible;

                foreach (var message in result)
                {
                    Errors += "\n" + message;
                }

                MessageBox.Show("В процессе проверки обнаружены ошибки!");



                return false;
            }

            try
            {

                // 1-ый способ:
                //if (NewStudent.Id > 0)
                //{
                //    using (var context = new CollegeEntities())
                //    {
                //        NewStudent.Group = new List<Group>();

                //        var studentFromDb = context.Student.FirstOrDefault(student => student.Id == NewStudent.Id);

                //        studentFromDb.Name = NewStudent.Name;
                //        studentFromDb.Age = NewStudent.Age;
                //        studentFromDb.Course = NewStudent.Course;
                //        studentFromDb.Group = NewStudent.Group;

                //        context.SaveChanges();

                //        return true;
                //    }
                //}


                // 2-ой способ:
                using (var context = new CollegeEntities())
                {
                    NewStudent.Group = new List<Group>();

                    var groupFromContext = context.Group.FirstOrDefault(group => group.Id == SelectedGroup.Id);

                    NewStudent.Group.Clear();

                    NewStudent.Group.Add(groupFromContext);
                    NewStudent.IsDeleted = false;
                    context.Student.AddOrUpdate(NewStudent);
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
