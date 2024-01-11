using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfEmployee.Models;

namespace WpfEmployee.ViewModels
{
     public class EmployeesVM
    {
        private List<EmployeesModel> _employeesList;
        private NorthwindContext context = new NorthwindContext();



        public List<EmployeesModel> EmployeesList
        {
            get { return _employeesList = _employeesList ?? loadEmployees(); }
        }

        private List<EmployeesModel> loadEmployees()
        {
            List<EmployeesModel> localCollection = new List<EmployeesModel>();
            foreach (var item in context.Employees)
            {
                localCollection.Add(new EmployeesModel(item));

            }

            return localCollection;

        }




    }
}
