using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfEmployee.Models;

namespace WpfEmployee.ViewModels
{
     public class EmployeesModel
    {
        private readonly Employee _employee;
        public EmployeesModel(Employee employee)
        {
            _employee = employee;
        }

        public String FullName
        {
            get { return _employee.LastName+" "+_employee.FirstName; }
            set { FullName=_employee.LastName + " " + _employee.FirstName; }


        }
      
        public String FirstName
        {
            get { return  _employee.FirstName; }
            set { FirstName=_employee.FirstName; }

        }
        // MessageBox.Show("ccsskc");
        public String LastName
        {
            get { return _employee.LastName; }
            set { LastName=_employee.FirstName; }
        }
        public DateTime? DisplayBirthDate
        {
            get { return _employee.BirthDate; }
            set { DisplayBirthDate= _employee.BirthDate; }
        }

        public DateTime? BirthDate
        {
            get { return _employee.BirthDate; }
            set { BirthDate = _employee.BirthDate; }
        }
    }
}
