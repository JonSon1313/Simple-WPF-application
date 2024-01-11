using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp_Practice.Infrastructure;
using WpfApp_Practice.Views;
namespace WpfApp_Practice.ViewModels
{
    internal class EditPositionWorkerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public EditPositionWorkerViewModel()
        {
            Workers = WorkerRepository.Workers;
            EditWorker = new Worker();
        }
        public ObservableCollection<Worker>? Workers { get; set; }
        public EditPositionWorkerViewModel(ObservableCollection<Worker>? workers, Worker selectedWorker)
        {
            Workers = workers;
            EditWorker = selectedWorker;
            WorkerToEdit = selectedWorker;
            NewPosition = EditWorker?.Position;
        }



        private string? newPosition;
        public string? NewPosition
        {
            get => newPosition;
            set
            {
                if (newPosition != value)
                {
                    newPosition = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Worker? WorkerToEdit
        {
            get;
            set;
        }

        private ICommand saveCommand;
        public ICommand SaveCommand => saveCommand ??= new RelayCommand(SaveMethod, CanSaveMethod);
    
        //Цей метод може виглядати жахливим, але все працює 
       private void SaveMethod(object? param)
       {
            if (WorkerToEdit != null)
            {
                // Оновлюємо посаду вибраного працівника зі значенням NewPosition
                WorkerToEdit.Position = NewPosition;
                // Отримуємо екземпляр ViewModel головного вікна
                var mainWindowViewModel = Application.Current.MainWindow?.DataContext as MainWindowViewModel;
                // Оповіщаємо, що змінилися дані вибраного працівника, оновлюючи його відображення в головному вікні
                mainWindowViewModel?.NotifyPropertyChanged(nameof(mainWindowViewModel.SelectedWorker));
               
            }
        }
        

        private bool CanSaveMethod(object? param)
        {
            return Workers?.All(item => item.Position != NewPosition) ?? false;
        }
        private ICommand? closeCommand;
        public ICommand? CloseCommand => closeCommand ??= new RelayCommand(param =>
        {
            if (param is Window window)
            {
                window.Close();
            }
        });
        public Worker? EditWorker { get; set; }

        private void NotifyPropertyChanged([CallerMemberName] string? name = null)
        {
            
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
