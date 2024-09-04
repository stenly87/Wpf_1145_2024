using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
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

namespace Wpf_1145_2024
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        static MainWindow Instance;
        public static MainWindow GetInstance() => Instance;

        private Page currentPage;

        void Signal([CallerMemberName] string prop = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public Page CurrentPage
        {
            get => currentPage;
            set
            {
                currentPage = value;
                Signal();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            var db = DataBase.GetInstance();
            Instance = this;
            DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void openEditor(object sender, RoutedEventArgs e)
        {
            CurrentPage = new NewStudentPage();
        }

        private void openTaskEditor(object sender, RoutedEventArgs e)
        {
            CurrentPage = new NewTaskPage();
        }
    }
}