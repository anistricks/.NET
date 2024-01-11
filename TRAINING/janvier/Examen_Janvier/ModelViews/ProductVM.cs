using Examen_Janvier.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApplication1.ViewModels;

namespace Examen_Janvier.ModelViews
{
    public class ProductVM : INotifyPropertyChanged
    {
        private NorthwindContext dc = new NorthwindContext();
        private ObservableCollection<ProductModel> _listProducts;
        private ObservableCollection<ProductModel> _productsByCountry;
        private ProductModel _selectedProduct;
        private DelegateCommand _discontinueCommand;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ProductModel SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; }
        }


       public ICommand DiscontinuedCommand
        {
            get { if (_discontinueCommand == null){
                    _discontinueCommand = new DelegateCommand(DiscontinueProduct);

                }
            return _discontinueCommand;
           }

        }

        private void DiscontinueProduct()
        {
            var selectedProduct = _selectedProduct;
            if(selectedProduct != null)
            {
                selectedProduct.Product.Discontinued = true;
                
                dc.SaveChanges();
                ProductList.Remove(selectedProduct);
                OnPropertyChanged(nameof(ProductList));


            }
            
        }

        public ObservableCollection<ProductModel> ProductList
        {
            get
            {
                return _listProducts ?? loadProducts();
            }

        }

        private ObservableCollection<ProductModel> loadProducts()
        {
            ObservableCollection<ProductModel> productslist = new ObservableCollection<ProductModel>();
            foreach (var product in dc.Products.Where(p=>p.Discontinued==false))
            {
                
                    productslist.Add(new ProductModel(product));
           
            }
            return productslist;


        }

        public ObservableCollection<ProductModel> ProductCountsByCountry
        {
            get { return _productsByCountry ?? loadOrderProduct(); }
        }

        private ObservableCollection<ProductModel> loadOrderProduct()
        {
            ObservableCollection<ProductModel> result = new ObservableCollection<ProductModel>();

            var countriesWithSales=dc.OrderDetails.Select(od=>od.Product.Supplier.Country).Distinct().ToList();
            foreach (var country in countriesWithSales)
            {
                var productCount = dc.OrderDetails.Where(od => od.Product.Supplier.Country == country)
                    .Select(od => od.ProductId).Distinct().Count();
                result.Add(new ProductModel(country,productCount));

            }

            return new ObservableCollection<ProductModel>(result.OrderByDescending(x=>x.Count)) ;

        }
    }
}
