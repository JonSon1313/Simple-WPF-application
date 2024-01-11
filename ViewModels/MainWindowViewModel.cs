using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using WpfApp_Practice.Infrastructure;
using WpfApp_Practice.Views;

namespace WpfApp_Practice.ViewModels
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private Worker? selectedWorker;
        public ObservableCollection<Worker>? Workers { get; set; }

        public ICollectionView? SortedWorkers { get; set; }
        
        private ICommand? sortCommand;
        public ICommand? SortCommand => sortCommand ??= new RelayCommand(SortMethod);

        private ICommand? filterCommand;
        public ICommand? FilterCommand => filterCommand ??= new RelayCommand(FilterMethod);

        private void FilterMethod(object? param)
        {

            if (SortedWorkers == null) return;

            if (param is TextBox tb)
            {
                SortedWorkers.Filter = item =>
                {
                    if (item is Worker worker)
                    {
                        return worker?.FirstName?.ToLower()?.Contains(tb.Text.ToLower()) ?? false;
                    }
                    return false;
                };
            }
        }
        
        
        public Worker? SelectedWorker
        {
            get => selectedWorker;
            set
            {
                if(value != selectedWorker) 
                {
                    selectedWorker = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public MainWindowViewModel()
        {
            Workers = WorkerRepository.Workers;
            SortedWorkers = CollectionViewSource.GetDefaultView(Workers);
            SortedWorkers.SortDescriptions.Add(new SortDescription("FirstName", ListSortDirection.Ascending));
        }

        private void SortMethod(object? parameter)
        {
            var column = parameter as GridViewColumnHeader;
            if (column == null) return;

            var dir = SortedWorkers?.SortDescriptions[0].Direction;
            var col = SortedWorkers?.SortDescriptions[0].PropertyName;
            var sortBy = column.Name.ToString();

            SortedWorkers?.SortDescriptions.Clear();
            if (sortBy == col && dir == ListSortDirection.Ascending)
            {
                SortedWorkers?.SortDescriptions.Add(new SortDescription(sortBy, ListSortDirection.Descending));
            }
            else
            {
                SortedWorkers?.SortDescriptions.Add(new SortDescription(sortBy, ListSortDirection.Ascending));
            }
        }
        private ICommand? openWindowCommand;
        public ICommand? OpenWindowCommand => openWindowCommand ??= new RelayCommand(OpenWindowMethod);
        private void OpenWindowMethod(object? param)
        {
            var win = new AddWorker();
            win.ShowDialog();
        }

        private ICommand? deleteCommand;

        public ICommand DeleteCommand => deleteCommand ??=new RelayCommand(DeleteMethod, param =>
        {
            return param is Worker || SelectedWorker != null;
        });
        private void DeleteMethod(object? param)
        {
            if (param is Worker prod)
            {
                Workers?.Remove(prod);
                return;
            }
            Workers?.Remove(SelectedWorker);
        }

        private ICommand? openEditCommand;
        public ICommand? OpenEditCommand => openEditCommand ??= new RelayCommand(openEditMethod);

        private void openEditMethod(object? param)
        {
            MessageBox.Show("OpenEditMethod called"); var win = new EditPosition(WorkerRepository.Workers, SelectedWorker);
            win.ShowDialog(); SortedWorkers?.Refresh();
        }
        private ICommand? closeCommand;
        public ICommand? CloseCommand => closeCommand ??= new RelayCommand(param =>
        {
            if (param is Window window)
            {
                window.Close();
            }
        });
        public void NotifyPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }

    
}
