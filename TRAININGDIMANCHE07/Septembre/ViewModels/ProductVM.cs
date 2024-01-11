using Septembre.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Septembre.ViewModels
{
    public class ProductVM
    {
        private NorthwindContext dc = new NorthwindContext();
        private ObservableCollection<ProductModel> _productsList;
        private ProductModel _selectedProduct;
        private DelegateCommand _majCommand;
        private ObservableCollection<TotalSalesModel> _totalSalesList;


        public ObservableCollection<ProductModel> ProductsList
        {
            get { return _productsList ?? LoadProduct(); }
        }

        public ProductModel SelectedProduct { get => _selectedProduct; set => _selectedProduct = value; }

        private ObservableCollection<ProductModel> LoadProduct()
        {
           ObservableCollection<ProductModel> localCollection=new ObservableCollection<ProductModel>();
            foreach(var p in dc.Products)
            {
                localCollection.Add(new ProductModel(p));
            }

            return localCollection;
        }
        public DelegateCommand MajCommand
        {
            get { return _majCommand ?? new DelegateCommand(UpdateProduct); }
        }

        private void UpdateProduct()
        {
            var selectedProduct = _selectedProduct;
            if (selectedProduct != null)
            {
                var productToUpdate = dc.Products.SingleOrDefault(p => p.ProductId ==selectedProduct.ProductID);
                if (productToUpdate != null)
                {
                    productToUpdate.ProductName = selectedProduct.ProductName;
                    productToUpdate.QuantityPerUnit= selectedProduct.QuantityPerUnit;
                    dc.SaveChanges();
                }

            }
        }

        public ObservableCollection<TotalSalesModel> TotalSalesList
        {
            get { return _totalSalesList ?? loadTotalSales(); }
        }

        private ObservableCollection<TotalSalesModel> loadTotalSales()
        {
            ObservableCollection<TotalSalesModel> localCollection=new ObservableCollection<TotalSalesModel>();
            foreach(var product in dc.Products)
            {
                var sum = dc.OrderDetails.Where(p => p.ProductId == product.ProductId)
                    .Sum(p=> p.UnitPrice*p.Quantity);

                localCollection.Add(new TotalSalesModel(product.ProductId,sum));



            }

            return localCollection;
        }
    }
}
