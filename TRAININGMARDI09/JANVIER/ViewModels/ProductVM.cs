using JANVIER.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JANVIER.ViewModels
{
    public class ProductVM : INotifyPropertyChanged
    {
        private NorthwindContext dc = new NorthwindContext();
        private ObservableCollection<ProductModel> _productsList;
        private ObservableCollection<ProductByCountryModel> _productByCountryList;

        private ProductModel _selectedProduct;
        private DelegateCommand _retireCommand;
        public event PropertyChangedEventHandler PropertyChanged; // La view s'enregistera automatiquement sur cet event
        public ProductModel SelectedProduct { get => _selectedProduct; set => _selectedProduct = value; }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); // On notifie que la propriété a changé
            }
        }

        

        public ObservableCollection<ProductModel> ProductsList
        {
            get { return _productsList ?? loadProducts(); }
        }

        private ObservableCollection<ProductModel> loadProducts()
        {
            ObservableCollection<ProductModel> localCollection = new ObservableCollection<ProductModel>();
            foreach(var product in dc.Products.Where(p=>p.Discontinued==false))
            {
                localCollection.Add(new ProductModel(product));
            }

            return localCollection;

        }

        public DelegateCommand RetireCommand
        {
            get { return _retireCommand ?? new DelegateCommand(RetireProduct); }
        }

      

        private void RetireProduct()
        {
            var selectedProduct = SelectedProduct;
            if (selectedProduct != null)
            {
                selectedProduct.Product.Discontinued = true;
                dc.SaveChanges();
                OnPropertyChanged("ProductsList");
            }
        }


        public ObservableCollection<ProductByCountryModel> ProductByCountryList
        {
            get { return _productByCountryList ?? LoadProduchByCountry(); }
        }

        private ObservableCollection<ProductByCountryModel> LoadProduchByCountry()
        {
            ObservableCollection<ProductByCountryModel> localCollection = new ObservableCollection<ProductByCountryModel>();
            var pays = dc.OrderDetails.Select(od => od.Product.Supplier.Country).Distinct().ToList();
            foreach (var p in pays)
            {
                var nb = dc.OrderDetails.Where(od => od.Product.Supplier.Country == p)
                    .Select(p => p.ProductId)
                    .Distinct()
                    .Count();

                localCollection.Add(new ProductByCountryModel(p, nb));
            }

            var descendingOrder = new ObservableCollection<ProductByCountryModel>(localCollection.OrderByDescending(a=>a.ProductByCountry));
            return descendingOrder;
        }
    }
}
