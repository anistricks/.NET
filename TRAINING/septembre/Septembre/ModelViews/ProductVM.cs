using Septembre.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApplication1.ViewModels;

namespace Septembre.ModelViews
{
    public class ProductVM : INotifyPropertyChanged
    {
        private NorthwindContext dc =new NorthwindContext();
        private ObservableCollection<ProductModel> _productsList;
        private ProductModel _selectedProduct;
        private DelegateCommand _majProductCommand;
        private ObservableCollection<ProductModel> _totalVentesParProduit;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ObservableCollection<ProductModel> ProductList
        {
            get { return _productsList ?? LoadProductList(); }
        }

        public ProductModel SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                if (_selectedProduct != value)
                {
                    _selectedProduct = value;
                    OnPropertyChanged(nameof(SelectedProduct));
                }
            }
        }
        public ICommand MajProductCommand
        {
            get
            {
                if (_majProductCommand == null)
                {
                    _majProductCommand = new DelegateCommand(MajProduct);
                }
                return _majProductCommand;
            }
         }

        private void MajProduct()
        {
            var selectedProduct = _selectedProduct;
            if(selectedProduct != null)
            {
                var productToUpdate = dc.Products.FirstOrDefault(p => p.ProductId == selectedProduct.ProductID);
                if (productToUpdate != null)
                {
                    productToUpdate.ProductName = selectedProduct.ProductName;
                    productToUpdate.QuantityPerUnit = selectedProduct.QuantityPerUnit;
                    dc.SaveChanges();
                }
            }
        }

        private ObservableCollection<ProductModel> LoadProductList()
        {
            ObservableCollection<ProductModel> listproducts = new ObservableCollection<ProductModel>();
            foreach(var product in dc.Products)
            {

                listproducts.Add(new ProductModel(product));    
            }
            return listproducts;
        }
        public ObservableCollection<ProductModel> TotalVentesParProduit
        {
            get { return _totalVentesParProduit ?? CalculateTotalSalesByProduct(); }
        }

        private ObservableCollection<ProductModel> CalculateTotalSalesByProduct()
        {
            /*var query = from Product p in dc.Products
                        join orderDetail in dc.OrderDetails on p.ProductId equals orderDetail.ProductId
                        group new { p , orderDetail } by p.ProductId into grouped
                        select new ProductModel(grouped.First().p)
                        {
                            ProductID = grouped.Key,
                            TotalSales = grouped.Sum(x => x.orderDetail.UnitPrice * x.orderDetail.Quantity),
                        };*/

            var query = from Product p in dc.Products
                        join orderDetail in dc.OrderDetails on p.ProductId equals orderDetail.ProductId
                        group new {p,orderDetail} by p.ProductId into grouped
                        select new ProductModel(grouped.First().p)
                        {
                            ProductID = grouped.Key,
                            TotalSales = grouped.Sum(x => x.orderDetail.UnitPrice * x.orderDetail.Quantity),
                        };
            return new ObservableCollection<ProductModel>(query);
        }
    }
}
