using SEPTEMBRE.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPTEMBRE.ViewModels
{
    public class ProductVM
    {
        private NorthwindContext dc = new NorthwindContext();
        private ObservableCollection<ProductModel> _productsList;
        private ObservableCollection<ProductByTotalSales> _productsByTotalSales;
        private ProductModel _selectedProduct;
        private DelegateCommand _majCommand;

        public ProductModel SelectedProduct { get => _selectedProduct; set => _selectedProduct = value; }

        public ObservableCollection<ProductModel> ProductsList
        {
            get { return _productsList ?? LoadProductList(); }
        }

        private ObservableCollection<ProductModel> LoadProductList()
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
            get { return _majCommand??new DelegateCommand(UpdateProduct); }
        }

        private void UpdateProduct()
        {
            var selectedProduct = _selectedProduct;
            if(selectedProduct != null) {
                var productToUpdate = dc.Products.SingleOrDefault(p => p.ProductId == selectedProduct.ProductID);
                productToUpdate.ProductName= selectedProduct.ProductName;
                productToUpdate.QuantityPerUnit= selectedProduct.QuantityPerUnit;
                dc.SaveChanges();

            
            }
        }


        public ObservableCollection<ProductByTotalSales> ProductsByTotalSales
        {
            get { return _productsByTotalSales ?? LoadProductBySales(); }

        }

        private ObservableCollection<ProductByTotalSales> LoadProductBySales()
        {
            ObservableCollection<ProductByTotalSales> localCollection = new ObservableCollection<ProductByTotalSales>();
            foreach(var p in dc.Products)
            {
                var sum = dc.OrderDetails.Where(product => product.ProductId == p.ProductId)
                    .Sum(s => s.Quantity * s.UnitPrice);

                localCollection.Add(new ProductByTotalSales(p.ProductId, sum)); 


            }

            return localCollection;
        }
    }
}
