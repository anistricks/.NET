using examen_janvier.Models;
using examen_janvier.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApplication1.ViewModels;

namespace examen_janvier.ViewsModels
{
    public class ProductVM : INotifyPropertyChanged
    {
        private ObservableCollection<ProductModel> _products;
        private NorthwindContext dc = new NorthwindContext();
        private ProductModel _selectedProduct;
        private DelegateCommand _discontinueCommand;

        private ObservableCollection<OrderModel> _orders;


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<ProductModel> LoadProduct()
        {
            ObservableCollection<ProductModel> produit = new ObservableCollection<ProductModel>();
            foreach (var p in dc.Products.Where(p => !p.Discontinued))
            {

                produit.Add(new ProductModel(p));


            }
            return produit;

        }

        public ObservableCollection<ProductModel> ProductsList {
            get { return _products ?? LoadProduct(); }


        }
        public ProductModel SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; }
        }

        public ICommand DiscontinueCommand
        {
            get
            {
                if (_discontinueCommand == null)
                {
                    _discontinueCommand = new DelegateCommand(DiscontinueProduct);
                }
                return _discontinueCommand;
            }
        }
        private void DiscontinueProduct()
        {

            var selectedProduct = _selectedProduct;


            if (selectedProduct != null)
            {

                selectedProduct.Product.Discontinued = true;
                dc.SaveChanges();
                ProductsList.Remove(selectedProduct);
                OnPropertyChanged(nameof(ProductsList));
            }
        }


        public ObservableCollection<OrderModel> ProductCountsByCountry
        {
            get { return _orders ?? LoadOrderProduct(); }
        }

        private ObservableCollection<OrderModel> LoadOrderProduct()
        {
            ObservableCollection<OrderModel> result = new ObservableCollection<OrderModel>();

            // recuperer les pays avec au moins une vente  car sinon ils seraient pas ds OrderDetails
            var countriesWithSales = dc.OrderDetails
                .Select(od => od.Product.Supplier.Country)
                .Distinct()
                .ToList();



            foreach (var country in countriesWithSales)
            {
                //compte produit vendu  unique par pays
                var productCount = dc.OrderDetails
                    .Where(od => od.Product.Supplier.Country == country)
                    .Select(od => od.ProductId)
                    .Distinct()
                    .Count();

                result.Add(new OrderModel(new Order { ShipCountry = country }, productCount));
            }

            //trier par nombre de vente
            return new ObservableCollection<OrderModel>(result.OrderByDescending(x => x.Count));
        }
    }
}
