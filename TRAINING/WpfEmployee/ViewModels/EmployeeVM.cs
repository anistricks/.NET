using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using WpfEmployee.Models;

namespace WpfEmployee.ViewModels
{
    class EmployeeVM : INotifyPropertyChanged
    {
        private ObservableCollection<EmployeeModel> _employees;
        private List<string> _titles;
        private ObservableCollection<OrderModel> _OrdersList;

        private EmployeeModel _selectedEmployee;

        private NorthwindContext context = new NorthwindContext();

        public event PropertyChangedEventHandler? PropertyChanged; // La view s'enregistera automatiquement sur cet event
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); // On notifie que la propriété a changé
            }
        }

        public ObservableCollection<EmployeeModel> LoadEmployees()
        {
            ObservableCollection<EmployeeModel> employees = new ObservableCollection<EmployeeModel>();

            foreach (var employee in context.Employees)
            {

                employees.Add(new EmployeeModel(employee));
            }

            return employees;

        }
        public List<string> LoadTitles()
        {
            List<string> titles = new List<string>();

            foreach (var employee in context.Employees)
            {
                if (!titles.Contains(employee.TitleOfCourtesy))
                {
                    titles.Add(employee.TitleOfCourtesy);

                }

            }

            return titles;

        }

        public List<string> ListTitle
        {
            get { return _titles ?? LoadTitles(); }

        }



        public ObservableCollection<EmployeeModel> EmployeesList
        {
            get { return _employees ?? LoadEmployees(); }

        }
        private ObservableCollection<OrderModel> loadOrders()
        {
            ObservableCollection<OrderModel> localCollection = new ObservableCollection<OrderModel>();
            var query = from Order o in context.Orders
                        where (o.EmployeeId == SelectedEmployee.MonEmployee.EmployeeId)
                        orderby o.OrderDate descending
                        select o;
            Console.WriteLine("rentré loadOrders ");


            int i = 0;
            foreach (var item in query)
            {
                decimal total = context.OrderDetails.Where(od => od.OrderId == item.OrderId).Sum(od => od.UnitPrice);
                localCollection.Add(new OrderModel(item, total));
                i++;
                if (i == 3) break;
                Console.WriteLine("rentré query ");
            }
            Console.WriteLine("rentré localCollection ");

            return localCollection;

        }

        public ObservableCollection<OrderModel> OrdersList
        {
            get
            {
                if (SelectedEmployee != null)
                {
                    _OrdersList = loadOrders();
                }
                Console.WriteLine("rentré OrdersList ");
                return _OrdersList;

            }

        }
        public EmployeeModel SelectedEmployee
        {

            get { return _selectedEmployee; Console.WriteLine("rentré SelectedEmployee "); }
            set { _selectedEmployee = value; OnPropertyChanged("OrdersList"); }


        }


    }
}
