using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public static class WorkerRepository
    {
        private static ObservableCollection<Worker>? workers;
        private static ObservableCollection<Worker> GenerateProducts()
        {
            return new ObservableCollection<Worker>()
            {
                new Worker() { FirstName = "Anton", LastName = "Antonovich", Position = "Administrator", BirthDate = new DateTime(1991,1,1) },
                new Worker() {FirstName="Oleksiy", LastName="Oleksiyovich", Position="Manager", BirthDate = new DateTime(1999, 3, 11) },
                new Worker() {FirstName="Petro", LastName="Petrovich", Position="Developer",  BirthDate = new DateTime(2001, 3, 2) },
                new Worker() {FirstName="Arcadiy", LastName="Arcadiyovich",   Position="Designer",  BirthDate = new DateTime(1985, 4, 9)},
                new Worker() {FirstName="Bogdan", LastName="Bogdanovich", Position="Analyst",  BirthDate = new DateTime(1987, 7, 8) }
            };
        }
        public static ObservableCollection<Worker> Workers => workers ??= GenerateProducts();
    }
}
