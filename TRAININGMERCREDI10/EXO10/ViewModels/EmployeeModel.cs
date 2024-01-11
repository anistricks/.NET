using EXO10.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXO10.ViewModels
{
    public class EmployeeModel : INotifyPropertyChanged
    {
        private readonly Employee _employee;
        public event PropertyChangedEventHandler PropertyChanged; // La view s'enregistera automatiquement sur cet event

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); // On notifie que la propriété a changé
            }
        }
        public EmployeeModel(Employee employee)
        {
            _employee = employee;
        }

        public Employee Employee => _employee;

        public string LastName
        {
            get { return _employee.LastName; }
            set { _employee.LastName=value; OnPropertyChanged("FullName"); }
        }
        public string FirstName
        {
            get { return _employee.FirstName; }
            set { _employee.FirstName = value; OnPropertyChanged("FullName"); }
        }
        public string Title
        {
            get { return _employee.TitleOfCourtesy; }
            set { _employee.TitleOfCourtesy = value; }
        }

        public DateTime? BirthDate
        {
            get { return _employee.BirthDate; }
            set { _employee.BirthDate = value;  }




        }

        public DateTime ? HireDate
        {
            get { return _employee.HireDate; }
            set {  _employee.HireDate=value; }


        }


        public string DisplayBirthdate
        {
            get { return  _employee.BirthDate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture); }

        }
        public string FullName
        {
            get { return _employee.FirstName + " " + _employee.LastName; }
        }
          
    }
}
