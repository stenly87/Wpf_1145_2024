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
using Wpf_1145_2024.Model.Student;
using Wpf_1145_2024.Model.Task;
using Wpf_1145_2024.MVVM;
using task = Wpf_1145_2024.Model.Task;
namespace Wpf_1145_2024
{

    /// <summary>
    /// Логика взаимодействия для ExecutorPage.xaml
    /// </summary>
    public partial class ExecutorPage : Page
    {
        public ObservableCollection<task.Task> Tasks { get; set; }
        public ObservableCollection<Student> Students { get; set; } = new();
        public task.Task SelectedTask { get; set; }

        public MVVMCommand DOIT { get; set; }
        public ExecutorPage()
        {
            InitializeComponent();
            ViewTasks();
            DOIT = new MVVMCommand(() =>
            {
                if (SelectedTask == null)
                    return;

                var allstudents = DB.DataBase.GetInstance().GetStudents();
                List<IEnumerable<Student>> array = new List<IEnumerable<Student>>();
                foreach (var attr in SelectedTask.Attributes)
                {
                    var stud = allstudents.Where(s => s.Attributes.FirstOrDefault(s => s.Title == attr.Attribute.Title) != null);
                    array.Add(stud);
                }
                var a = array.First();
                foreach (var b in array)
                {
                    a = a.Intersect(b);
                }
                Students.Clear();
                foreach (var b in a)
                    Students.Add(b);
            });
            DataContext = this;
        }

        private void ViewTasks()
        {
            Tasks = new ObservableCollection<task.Task>(DataBase.GetInstance().GetTasks());
        }
    }
}
