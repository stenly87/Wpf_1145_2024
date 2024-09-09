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
using task = Wpf_1145_2024.Model.Task;
using Wpf_1145_2024.MVVM;
using Wpf_1145_2024.Model.Student;
using Wpf_1145_2024.Model;
using Wpf_1145_2024.Model.Task;
using Wpf_1145_2024.DB;

namespace Wpf_1145_2024
{
    /// <summary>
    /// Логика взаимодействия для NewTaskPage.xaml
    /// </summary>
    public partial class NewTaskPage : Page
    {
        public MVVMCommand Save { get; set; }
        public task.Task Task { get; set; } = new();
        public ObservableCollection<string> TaskAttributes { get; set; } = new();

        public ObservableCollection<IModel> NewAttribute { get; set; } = new();
        public ObservableCollection<task.Task> Tasks { get; set; }

        public NewTaskPage()
        {
            InitializeComponent();
            ViewTasks();
            Save = new MVVMCommand(() => SaveAttribute(null, null));
            DataContext = this;
        }

        private void ViewTasks()
        {
            Tasks = new ObservableCollection<task.Task>(DataBase.GetInstance().GetTasks());
        }

        private void SaveAttribute(object value1, object value2)
        {
            var attr = (IStudentAttribute)NewAttribute.FirstOrDefault(s=>s is IStudentAttribute);
            if (string.IsNullOrEmpty(attr.Title))
                return;

            if (attr != null)
            {
                var delete = Task.Attributes.FirstOrDefault(s => s.Attribute.Title == attr.Title);
                if (delete != null)
                {
                    TaskAttributes.Remove(delete.ToString());
                    Task.Attributes.Remove(delete);
                }
                var accuracy = NewAttribute.FirstOrDefault(s => s is InaccuracyTask);
                float inaccuracyTask = accuracy != null ? ((InaccuracyTask)accuracy).Value : 0.0f;
                var taskAttribute = new TaskAttribute { Attribute = attr, Inaccuracy = inaccuracyTask };
                Task.Attributes.Add(taskAttribute);
                TaskAttributes.Add(taskAttribute.ToString());
            }
            NewAttribute.Clear();
        }

        private void SaveAndClose(object sender, RoutedEventArgs e)
        {
            var db = DB.DataBase.GetInstance();
            db.Add(Task);
            db.Save();
            MainWindow.GetInstance().CurrentPage = null;
        }

        private void newRangeAttribute(object sender, RoutedEventArgs e)
        {
            NewAttribute.Clear();
            NewAttribute.Add(new StudentAttributeRange());
            NewAttribute.Add(new InaccuracyTask());
        }

        private void newFloatAttribute(object sender, RoutedEventArgs e)
        {
            NewAttribute.Clear();
            NewAttribute.Add(new StudentAttributeFloat());
            NewAttribute.Add(new InaccuracyTask());
        }

        private void newStringAttribute(object sender, RoutedEventArgs e)
        {
            NewAttribute.Clear();
            NewAttribute.Add(new StudentAttributeString());
        }

        private void newBoolAttribute(object sender, RoutedEventArgs e)
        {
            NewAttribute.Clear();
            NewAttribute.Add(new StudentAttributeBool());
        }
    }
}
