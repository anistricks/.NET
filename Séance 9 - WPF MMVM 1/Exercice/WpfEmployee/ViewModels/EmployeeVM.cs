using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfEmployee.Models;

namespace WpfEmployee.ViewModels
{
    public class EmployeeVM
    {
        private List<string> _listTitle;
        private List<EmployeeModel> _employees;
        private NorthwindContext dc = new NorthwindContext();


        public List<EmployeeModel> EmployeesList
        {
            get { return _employees ?? LoadEmployees(); }
        }

        private List<EmployeeModel> LoadEmployees()
        {
            List<EmployeeModel> loadingCollection = new List<EmployeeModel>();
            foreach (var item in dc.Employees)
            {
                loadingCollection.Add(new EmployeeModel(item));
            }


            return loadingCollection;

        }

        public List<string> ListTitle
        {
            get { return _listTitle = _listTitle ?? LoadTitleOfCourtesy(); }
        }

        private List<string>? LoadTitleOfCourtesy()
        {

#pragma warning disable CS8619 // La nullabilité des types référence dans la valeur ne correspond pas au type cible.
            return  dc.Employees.Select(e=>e.TitleOfCourtesy).Distinct().ToList();
#pragma warning restore CS8619 // La nullabilité des types référence dans la valeur ne correspond pas au type cible.
        }


    }

}