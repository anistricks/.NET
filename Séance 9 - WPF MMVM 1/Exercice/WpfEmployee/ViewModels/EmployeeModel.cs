using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfEmployee.Models;

namespace WpfEmployee.ViewModels
{
    public class EmployeeModel
    {
        private readonly Employee _employee;
      

        public EmployeeModel(Employee employee)
        {
            _employee = employee;
        }



        public string FullName
        {
            get
            {
                return _employee.FirstName + " " + _employee.LastName;
            }
            set { FullName = _employee.LastName + " " + _employee.FirstName; }
        }

        public String DisplayBirthDate
        {
            get
            {
                if (_employee.BirthDate.HasValue)
                {
                    return _employee.BirthDate.Value.Day+"/"+ _employee.BirthDate.Value.Month+"/"+ _employee.BirthDate.Value.Year;
                }

                return "";
            }
        }


        public string LastName
        {
            get
            {
                return  _employee.LastName;
            }
            set { LastName = _employee.FirstName; }
        }

        public string FirstName
        {
            get
            {
                return _employee.FirstName ;
            }
            set { FirstName = _employee.FirstName; }
        }

        public DateTime? BirthDate
        {
            get
            {
                return _employee.BirthDate;
            }
            set { BirthDate = _employee.BirthDate; }
        }

        public DateTime? HireDate
        {
            get
            {
                return _employee.HireDate;
            }
            set { _employee.HireDate = _employee.HireDate; }
        }
        public string TitleOfCourtesy
        {
            get
            {
                return _employee.TitleOfCourtesy;
            }
            set { _employee.TitleOfCourtesy = _employee.TitleOfCourtesy; }

        }

    }
}
