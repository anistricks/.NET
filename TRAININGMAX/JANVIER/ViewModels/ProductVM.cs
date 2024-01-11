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
        private ObservableCollection<ProductsByCountryModel> _productsByCountryList;
        private ProductModel _selectedProduct;
        private DelegateCommand _retrieveProduct;
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
            get { return _productsList ?? LoadProduct(); }
        }

        private ObservableCollection<ProductModel> LoadProduct()
        {
            ObservableCollection<ProductModel> localCollection = new ObservableCollection<ProductModel>();
            foreach(var product in dc.Products.Where(p => p.Discontinued == false))
            {
                localCollection.Add(new ProductModel(product));
            }
            return localCollection;
        }

        public DelegateCommand DeleteCommand
        {
            get { return _retrieveProduct ?? new DelegateCommand(DeleteProduct); }
        }

        private void DeleteProduct()
        {
          var selectedProduct=_selectedProduct;
            if(selectedProduct != null)
            {
                selectedProduct.Product.Discontinued = true;
                dc.SaveChanges();
                OnPropertyChanged("ProductsList");


            }
        }
       public  ObservableCollection<ProductsByCountryModel> ProductsByCountryList
        {
            get { return _productsByCountryList ?? LoadProductByCountry(); }
        }

        private ObservableCollection<ProductsByCountryModel> LoadProductByCountry()
        {
            ObservableCollection<ProductsByCountryModel> localCollection = new ObservableCollection<ProductsByCountryModel>();

            var pays =dc.OrderDetails.Select(od=>od.Product.Supplier.Country).Distinct().ToList();
            foreach(var p in pays)
            {
                var nb = dc.OrderDetails.Where(od => od.Product.Supplier.Country == p)
                    .Select(s => s.ProductId).Distinct().Count();

                localCollection.Add(new ProductsByCountryModel(p, nb));
            }
            var OrdreDecroissant = new ObservableCollection<ProductsByCountryModel>
                (localCollection.OrderByDescending(ob=>ob.NumberOfProduct));

            return OrdreDecroissant;

        }
    }
}
