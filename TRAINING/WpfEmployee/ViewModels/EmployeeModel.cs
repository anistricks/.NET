using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfEmployee.Models;

namespace WpfEmployee.ViewModels
{
    class EmployeeModel : INotifyPropertyChanged
    {
        private readonly Employee _employee;
        public Employee MonEmployee
        {
            get { return _employee; }
        }

        public EmployeeModel(Employee employee)
        {
               _employee = employee;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        public virtual void OnPropertyChanged (string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));  
            }
        }

        public string FullName
        {
            get {   return _employee.FirstName+" "+_employee.LastName; }

        
        }
        public string DisplayBirthDate
        {

            get {return _employee.BirthDate.Value.Day+"/"+ _employee.BirthDate.Value.Month+"/"+ _employee.BirthDate.Value.Year; }
        }

        public string LastName
        {
            get { return _employee.LastName; }
            set { _employee.LastName = value; OnPropertyChanged("FullName"); }
        }

        public string FirstName
        {
            get { return _employee.FirstName; } 
            set { _employee.FirstName = value; OnPropertyChanged("FullName"); }
        }

        public DateTime? BirthDate
        {
            get { return _employee.BirthDate.Value; }
            set { _employee.BirthDate = value; OnPropertyChanged("DisplayBirthDate"); }
        }

        public DateTime? HireDate
        {
            get { return _employee.HireDate.Value; }
            set { _employee.HireDate = value; }
        }


        public string? TitleOfCourtesy
        {
            get { return _employee.TitleOfCourtesy; }
            set { _employee.TitleOfCourtesy = value; }

        }

      
    }
}
