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
using Wpf_1145_2024.DB;
using Wpf_1145_2024.Model;
using Wpf_1145_2024.Model.Student;
using Wpf_1145_2024.MVVM;

namespace Wpf_1145_2024
{
    /// <summary>
    /// Логика взаимодействия для NewStudentPage.xaml
    /// </summary>
    public partial class NewStudentPage : Page
    {
        public MVVMCommand Save { get; set; }
        public Student Student { get; set; } = new();
        public ObservableCollection<string> StudentAttributes { get; set; } = new();

        public ObservableCollection<IStudentAttribute> NewAttribute { get; set; } = new();

        public ObservableCollection<Student> Students { get; set; }
        public NewStudentPage()
        {
            InitializeComponent();
            ViewStudents();
            Save = new MVVMCommand(() => SaveAttribute(null, null));
            DataContext = this;
        }

        private void ViewStudents()
        {
            Students = new ObservableCollection<Student>(DataBase.GetInstance().GetStudents());
        }

        private void newBoolAttribute(object sender, RoutedEventArgs e)
        {
            NewAttribute.Clear();
            NewAttribute.Add(new StudentAttributeBool());
        }

        private void newStringAttribute(object sender, RoutedEventArgs e)
        {
            NewAttribute.Clear();
            NewAttribute.Add(new StudentAttributeString());
        }

        private void newFloatAttribute(object sender, RoutedEventArgs e)
        {
            NewAttribute.Clear();
            NewAttribute.Add(new StudentAttributeFloat());
        }

        private void newRangeAttribute(object sender, RoutedEventArgs e)
        {
            NewAttribute.Clear();
            NewAttribute.Add(new StudentAttributeRange());
        }

        private void SaveAttribute(object sender, RoutedEventArgs e)
        {
            var attr = NewAttribute.FirstOrDefault();
            if (string.IsNullOrEmpty(attr.Title))
                return;

            if (attr != null)
            {
                var delete = Student.Attributes.FirstOrDefault(s => s.Title == attr.Title);
                if (delete != null)
                {
                    StudentAttributes.Remove(delete.ToString());
                    Student.Attributes.Remove(delete);
                }
                Student.Attributes.Add(attr);
                StudentAttributes.Add(attr.ToString());
            }
            NewAttribute.Clear();
        }

        private void SaveAndClose(object sender, RoutedEventArgs e)
        {
            var db = DB.DataBase.GetInstance();
            db.Add(Student);
            db.Save();
            MainWindow.GetInstance().CurrentPage = null;
        }


    }
}
