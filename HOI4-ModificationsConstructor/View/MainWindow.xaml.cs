using System.Windows;

namespace HOI4_ModificationsConstructor
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new StateViewModel(new DefaultDialogService(), new DefaultFileService());
        }
    }
}