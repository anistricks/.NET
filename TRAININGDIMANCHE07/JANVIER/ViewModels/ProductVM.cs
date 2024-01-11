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
        private ProductModel _productSelected;
        private DelegateCommand _retireProduct;
        public event PropertyChangedEventHandler PropertyChanged; // La view s'enregistera automatiquement sur cet event
        private ObservableCollection<ProductBySalesCountryModel> _productByCountry;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); // On notifie que la propriété a changé
            }
        }

        public ObservableCollection<ProductModel> ProductsList
        {
            get { return _productsList ?? loadProduct(); }
        }

        public ProductModel ProductSelected { get => _productSelected; set => _productSelected = value; }

        private ObservableCollection<ProductModel> loadProduct()
        {
            ObservableCollection<ProductModel> localCollection = new ObservableCollection<ProductModel>();
            foreach(var p in dc.Products.Where(p => p.Discontinued == false)){

                localCollection.Add(new ProductModel(p));

            }
            return localCollection;
        }

        public DelegateCommand RetireCommand
        {
            get { return _retireProduct ?? new DelegateCommand(retireProduct); }
        }

        private void retireProduct()
        {
            var selectedProduct = _productSelected;
            if(selectedProduct != null)
            {
                selectedProduct.Product.Discontinued= true;
                ProductsList.Remove(selectedProduct);
                dc.SaveChanges();
              

                OnPropertyChanged("ProductsList");


            }
        }

        public ObservableCollection<ProductBySalesCountryModel> ProductByCountry
        {
            get { return _productByCountry ?? loadProductByCountry(); }
        }

        private ObservableCollection<ProductBySalesCountryModel> loadProductByCountry()
        {
            ObservableCollection<ProductBySalesCountryModel> localCollection = new ObservableCollection<ProductBySalesCountryModel>();
            var pays = dc.OrderDetails.Select(p => p.Product.Supplier.Country).Distinct().ToList(); 
            foreach( var pay in pays)
            {

                var count = dc.OrderDetails.Where(od=>od.Product.Supplier.Country==pay)
                    .Select(p=>p.ProductId)
                    .Distinct().Count();


                localCollection.Add(new ProductBySalesCountryModel(pay, count));

            }
            var newCollection= new ObservableCollection<ProductBySalesCountryModel>(localCollection.OrderByDescending(o=>o.NumberProduct));



            return newCollection;
        }
    }
}
