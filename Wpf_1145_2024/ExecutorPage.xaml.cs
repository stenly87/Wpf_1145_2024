using MinimumEditDistance;
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
                List<(Student, IStudentAttribute)> array2 = new List<(Student, IStudentAttribute)>();
                foreach (var attr in SelectedTask.Attributes)
                {
                    bool first;
                    var stud = allstudents.Where(s =>
                    {
                        var attribute = s.Attributes.FirstOrDefault(s =>
                            Levenshtein.CalculateDistance(s.Title, attr.Attribute.Title, 1) <= 6);

                        if (attribute == null)
                            return false;

                        first = Math.Abs(attr.Attribute.GetFloatValue() - attribute.GetFloatValue()) <= attr.Inaccuracy;
                        if (!first)
                            array2.Add((s, attribute));
                        return first;
                    });
                    array.Add(stud);
                }
                var a = array.First();
                foreach (var b in array)
                    a = a.Intersect(b);

                if (a.Count() == 0)
                {
                    var students = array2.Select(s=>s.Item1).Distinct().ToList();
                    if (students.Count() != 0)
                    {
                        var test = students.Select(s => (s, array2.Where(a => a.Item1 == s).Sum(a => a.Item2.GetFloatValue()))).ToList();
                        test.Sort((x, y) => x.Item2.CompareTo(y.Item2));
                        a = test.Select(s => s.s);
                    }
                    else
                        MessageBox.Show("Не хватает людей. Некому поручить");
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
