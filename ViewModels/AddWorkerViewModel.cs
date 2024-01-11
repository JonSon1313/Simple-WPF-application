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
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using WpfApp_Practice.Infrastructure;
namespace WpfApp_Practice.ViewModels
{
    internal class AddWorkerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public AddWorkerViewModel()
        {
            Workers = WorkerRepository.Workers;
            NewWorker = new Worker();
            NewBirthDate = DateTime.Now;
        }

        public ObservableCollection<Worker>? Workers { get; set; }
        public AddWorkerViewModel(ObservableCollection<Worker>? workers)
        {
            Workers = workers;
            NewWorker = new Worker();
        }

        private string? newFirstName;
        public string? NewFirstName
        {
            get => newFirstName;
            set
            {
                if (newFirstName != value)
                {
                    newFirstName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string? newLastName;
        public string? NewLastName
        {
            get => newLastName;
            set
            {
                if (newLastName != value)
                {
                    newLastName = value;
                    NotifyPropertyChanged();
                }
            }
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
        private DateTime newBirthDate;
        public DateTime NewBirthDate
        {
            get => newBirthDate;
            set
            {
                if (newBirthDate != value)
                {
                    newBirthDate = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private ICommand addCommand;
        public ICommand AddCommand => addCommand ??= new RelayCommand(AddMethod, CanAddMethod);

        private void AddMethod(object? param)
        {
            var newWorker = new Worker()
            {
                FirstName = NewFirstName,
                LastName = NewLastName,
                Position = NewPosition,
                BirthDate = NewBirthDate
            };

            Workers?.Add(newWorker);

        }

        private bool CanAddMethod(object? param)
        {
            foreach (var item in Workers)
            {
                if (item.FirstName == NewFirstName &&
                    item.LastName == NewLastName &&
                    item.Position == NewPosition &&
                    item.BirthDate == NewBirthDate)
                    return false;
            }
            return true;
        }
        private ICommand? closeCommand;
        public ICommand? CloseCommand => closeCommand ??= new RelayCommand(param =>
        {
            if (param is Window window)
            {
                window.Close();
            }
        });

        public Worker? NewWorker { get; set; }

        private void NotifyPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
