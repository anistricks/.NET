using EXO10.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXO10.ViewModels
{
    public class EmployeeVM : INotifyPropertyChanged
    {
        private NorthwindContext dc = new NorthwindContext();
        private ObservableCollection<EmployeeModel> _employeesList;
        private ObservableCollection<OrderModel> _ordersList;
        private List<string> _listTitle;

        private EmployeeModel _selectedEmployee;
        private DelegateCommand _saveCommand;
        public event PropertyChangedEventHandler PropertyChanged; // La view s'enregistera automatiquement sur cet event

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); // On notifie que la propriété a changé
            }
        }

        public EmployeeModel SelectedEmployee
        {
            get { return _selectedEmployee; }
            set { _selectedEmployee = value; OnPropertyChanged("OrdersList"); }
        }


        public ObservableCollection<EmployeeModel> EmployeesList
        {
            get { return _employeesList ?? LoadEmployeeList(); }
        }

        private ObservableCollection<EmployeeModel> LoadEmployeeList()
        {
            ObservableCollection<EmployeeModel> localCollection = new ObservableCollection<EmployeeModel>();
            foreach(var employee in dc.Employees)
            {
                localCollection.Add(new EmployeeModel(employee));
            }
            return localCollection;
        }

        public DelegateCommand SaveCommand
        {
            get { return _saveCommand ?? new DelegateCommand(SaveEmployee); }
        }

        private void SaveEmployee()
        {
            var selectedEmployee = _selectedEmployee;
            if (selectedEmployee != null)
            {
                var employeeToUpdate = dc.Employees.FirstOrDefault(e => e.EmployeeId == selectedEmployee.Employee.EmployeeId);
                employeeToUpdate.LastName=selectedEmployee.LastName;
                employeeToUpdate.FirstName=selectedEmployee.FirstName;
                dc.SaveChanges();

            }
        }

        public ObservableCollection<OrderModel> OrdersList
        {
            get { return _ordersList ?? LoadOrdersList(); }
        }

        private ObservableCollection<OrderModel> LoadOrdersList()
        {
            ObservableCollection<OrderModel> localColletion = new ObservableCollection<OrderModel>();
            var selectedEmployee = _selectedEmployee;
            if(selectedEmployee != null)
            {
                int i = 0;
                var orderOfEmployee=dc.Orders.Where(or=>or.EmployeeId==selectedEmployee.Employee.EmployeeId).OrderByDescending(d=>d.OrderDate).ToList();

                foreach(var ord in orderOfEmployee)
                {
                    var sum = dc.OrderDetails.Where(od => od.Order == ord).Sum(q => q.UnitPrice);
                   

                    i++;
                    localColletion.Add(new OrderModel(ord, sum));
                    if (i == 3)
                    {
                        break;
                    }
                }


            }
            return localColletion;
        }


        public List<string> ListTitle
        {
            get { return dc.Employees.Select(e => e.TitleOfCourtesy).ToList(); }
        }
    }
}
