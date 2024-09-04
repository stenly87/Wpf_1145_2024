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

        public NewTaskPage()
        {
            InitializeComponent();
            Save = new MVVMCommand(() => SaveAttribute(null, null));
            DataContext = this;
        }

        private void SaveAttribute(object value1, object value2)
        {
            throw new NotImplementedException();
        }

        private void SaveAndClose(object sender, RoutedEventArgs e)
        {

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
