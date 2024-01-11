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

        private NorthwindContext dc=new NorthwindContext();
        private ObservableCollection<ProductModel> _productsList;
        private ProductModel _selectedProduct;
        private DelegateCommand _removeCommand;
        private ObservableCollection<ProductByCountryModel> _productsByCountry;
        public event PropertyChangedEventHandler PropertyChanged; // La view s'enregistera automatiquement sur cet event

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); // On notifie que la propriété a changé
            }
        }

        public ObservableCollection<ProductModel> ProductsList
        {
            get { return _productsList ?? LoadProduct(); }
        }

        private ObservableCollection<ProductModel> LoadProduct()
        {
            ObservableCollection<ProductModel> localCollection=new ObservableCollection<ProductModel>();
            foreach(var p in dc.Products.Where(p => p.Discontinued == false))
            {
                localCollection.Add(new ProductModel(p));
            }
            return localCollection; 
        }

        public DelegateCommand RemoveCommand
        {
            get { return _removeCommand ?? new DelegateCommand(DeleteProduct); }
        }

        private void DeleteProduct()
        {
            var selectedProduct = SelectedProduct;
            if (selectedProduct != null)
            {
                selectedProduct.Product.Discontinued = true;
                ProductsList.Remove(selectedProduct);
                dc.SaveChanges();
                OnPropertyChanged("ProductsList");

            }
        }

        public ObservableCollection<ProductByCountryModel> ProductByCountry
        {
            get { return _productsByCountry ?? LoadProductByCountry(); }
        }

        public ProductModel SelectedProduct { get => _selectedProduct; set => _selectedProduct = value; }

        private ObservableCollection<ProductByCountryModel> LoadProductByCountry()
        {
            ObservableCollection<ProductByCountryModel> localCollection = new ObservableCollection<ProductByCountryModel>();
            var listCountry =dc.OrderDetails.Select(od=>od.Product.Supplier.Country).Distinct().ToList();
            foreach(var country in listCountry)
            {
                var count = dc.OrderDetails.Where(od=>od.Product.Supplier.Country==country)
                              .Select(p=>p.ProductId)
                              .Distinct()
                              .Count();


            localCollection.Add(new ProductByCountryModel(country, count));
            }
            var  decroissantNbProduit=new ObservableCollection<ProductByCountryModel>(localCollection.OrderByDescending(l=>l.NumberOfProduct));
            return decroissantNbProduit;


        }
    }
}
