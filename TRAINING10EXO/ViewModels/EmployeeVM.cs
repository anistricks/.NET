using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TRAINING10EXO.Models;
using WpfApplication1.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TRAINING10EXO.ViewModels
{
    public class EmployeeVM : INotifyPropertyChanged
    {
        private NorthwindContext dc = new NorthwindContext();
        private ObservableCollection<EmployeeModel> _listEmployees;
      
        private ObservableCollection<OrderModel> _OrdersList;
        private EmployeeModel _selectedEmployee;
        private List<string> _listTittle;
        private DelegateCommand _addCommand;
        private DelegateCommand _saveCommand;
        private DelegateCommand _removeCommand;



        public event PropertyChangedEventHandler PropertyChanged; // La view s'enregistera automatiquement sur cet event
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); // On notifie que la propriété a changé
            }
        }
        public EmployeeModel SelectedEmployee {
            get { return _selectedEmployee; }
            set { _selectedEmployee = value;OnPropertyChanged("OrdersList"); }

        }

        public ObservableCollection<EmployeeModel> EmployeeList
        {
            get { return _listEmployees ?? loadEmployees(); }
        }

        private ObservableCollection<EmployeeModel> loadEmployees()
        {
            ObservableCollection<EmployeeModel> localCollection = new ObservableCollection<EmployeeModel>();
            foreach (var emp in dc.Employees)
            {
                
                localCollection.Add(new EmployeeModel(emp)); }
         
            return localCollection;
        }

        public List<string> ListTitle
        {
            get { return _listTittle ?? loadTitles(); }
        }

        private List<string> loadTitles()
        {
            return dc.Employees.Select(emp => emp.TitleOfCourtesy).Distinct().ToList();

        }


        public DelegateCommand AddCommand
        {
            get {return _addCommand=_addCommand ??new DelegateCommand(AddEmployee); }
         }
        public DelegateCommand SaveCommand
        {
            get { return _saveCommand = _saveCommand ?? new DelegateCommand(SaveEmployee); }
        }
        public DelegateCommand RemoveCommand
        {
            get {
                if (_removeCommand == null)
                {
                    _removeCommand = new DelegateCommand(RemoveEmployee);
                }
                return _removeCommand;
            }
        }

        private void RemoveEmployee()

        {
            var selectedEmployee = _selectedEmployee;
            if(selectedEmployee != null)
            {
                EmployeeList.Remove(selectedEmployee);
                OnPropertyChanged("EmployeeList");

          

            }
        }

        private void SaveEmployee()
        {
            
       
        Employee verif = dc.Employees.Where(e => e.EmployeeId == SelectedEmployee.Employee.EmployeeId).SingleOrDefault();
            if(verif == null )
            {
                dc.Employees.Add(SelectedEmployee.Employee);

            }
            dc.SaveChanges();
            MessageBox.Show("Enregistrement en base de données realisé ! ");
           
        }

        private void AddEmployee()
        {

            Employee globalEmp = new Employee();
            EmployeeModel empModel = new EmployeeModel(globalEmp);
            EmployeeList.Add(empModel);

            SelectedEmployee = empModel;
        }
           

        public ObservableCollection<OrderModel> OrdersList
        {

            get
            {

                if (SelectedEmployee != null) { 
                 _OrdersList = loadOrders();
                }
                return _OrdersList;


            }
        }
             private ObservableCollection<OrderModel> loadOrders()
             {
          
            ObservableCollection<OrderModel> localCollection = new ObservableCollection<OrderModel>();

                var query=dc.Orders
                .Where(o=>o.EmployeeId==SelectedEmployee.Employee.EmployeeId)
                .OrderByDescending(o=>o.OrderDate)
                .Select(o=>o);
            int i = 0;
            
            foreach (var o in query)
            {
                decimal total=dc.OrderDetails.Where(od=>od.OrderId==o.OrderId).Sum(od=>od.UnitPrice);
             
                localCollection.Add(new OrderModel(o, total));
                i++;
                if(i==3) { 
                    break;
                }



            }
           
            return localCollection;


                 }

        

    }
}
