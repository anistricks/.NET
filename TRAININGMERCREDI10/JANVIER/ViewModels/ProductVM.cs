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
        private ObservableCollection<ProductByCountry> _productsByCountryList;
        private ProductModel _selectedProduct;
        private DelegateCommand _retrieveCommand;
        public event PropertyChangedEventHandler PropertyChanged; // La view s'enregistera automatiquement sur cet event

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); // On notifie que la propriété a changé
            }
        }

        public ProductModel SelectedProduct { get => _selectedProduct; set => _selectedProduct = value; }

        public ObservableCollection<ProductModel> ProductsList
        {
            get { return _productsList ?? LoadProductsList(); }
        }

        private ObservableCollection<ProductModel> LoadProductsList()
        {
            ObservableCollection<ProductModel> localCollection = new ObservableCollection<ProductModel>();
            foreach (var prod in dc.Products.Where(p=>p.Discontinued==false))
            {

                localCollection.Add(new ProductModel(prod));


            }
            return localCollection;
        }

        public DelegateCommand RetrieveCommand
        {
            get { return _retrieveCommand ?? new DelegateCommand(DeleteProductFromList); }
        }

        private void DeleteProductFromList()
        {
            var productSelected = _selectedProduct;
            if(productSelected != null)
            {
                productSelected.Product.Discontinued = true;
                ProductsList.Remove(productSelected);
                dc.SaveChanges();
                OnPropertyChanged("ProductsList");

            }
        }

        public ObservableCollection<ProductByCountry> ProductsByCountryList
        {
            get { return _productsByCountryList ?? LoadProductByCountry(); }
        }

        private ObservableCollection<ProductByCountry> LoadProductByCountry()
        {
            ObservableCollection<ProductByCountry> localCollection = new ObservableCollection<ProductByCountry>();
            var pays = dc.OrderDetails.Select(od => od.Product.Supplier.Country).Distinct().ToList();
            foreach (var item in pays)
            {
                var nb = dc.OrderDetails.Where(od => od.Product.Supplier.Country == item)
                    .Select(p => p.ProductId).Distinct().Count();

                localCollection.Add(new ProductByCountry(item, nb));
            }
            var orderedByDescending=new ObservableCollection<ProductByCountry>(localCollection.OrderByDescending(am=>am.NumberOfProduct));
            return orderedByDescending;
        }
    }
}
