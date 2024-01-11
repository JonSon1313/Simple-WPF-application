using Model;
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
using System.Windows.Shapes;
using WpfApp_Practice.ViewModels;

namespace WpfApp_Practice.Views
{
    /// <summary>
    /// Логика взаимодействия для EditPosition.xaml
    /// </summary>
    public partial class EditPosition : Window
    {
        public EditPosition()
        {
            InitializeComponent();
            DataContext = new EditPositionWorkerViewModel();
        }

        public EditPosition(ObservableCollection<Worker>? workers, Worker selectedWorker)
        {
            InitializeComponent();
            DataContext = new EditPositionWorkerViewModel(workers, selectedWorker);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
