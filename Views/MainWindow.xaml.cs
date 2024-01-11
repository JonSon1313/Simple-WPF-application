using Model;
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
using WpfApp_Practice.ViewModels;

namespace WpfApp_Practice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var collection = (DataContext as MainWindowViewModel)?.SortedWorkers;
            if (collection == null) return;

            if (sender is TextBox tb)
            {
                collection.Filter = item =>
                {
                    if (item is Worker worker)
                    {
                        return worker?.FirstName?.ToLower()?.Contains(tb.Text.ToLower()) ?? false;
                    }
                    return false;
                };
            }
        }
    }
}