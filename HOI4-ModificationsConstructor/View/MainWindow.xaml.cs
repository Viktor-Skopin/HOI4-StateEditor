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

            //Установка StateViewModel в качестве контекста данного окна.
            DataContext = new StateViewModel(new StateDialogService(), new StateFileService());
        }
    }
}