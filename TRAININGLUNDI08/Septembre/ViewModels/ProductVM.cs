using Septembre.Models;
using Septembre.ViewModel;
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
        private NorthwindContext dc=new NorthwindContext();
        private ObservableCollection<ProductModel> _productsList;
        private ProductModel _selectedProduct;
        private DelegateCommand _majCommand;
        private ObservableCollection<TotalSalesByProductModel> _totalSalesByProductList;

        public ProductModel SelectedProduct { get => _selectedProduct; set => _selectedProduct = value; }


        public ObservableCollection<ProductModel> ProductsList
        {
            get { return _productsList ?? LoadProduct(); }

        }

        private ObservableCollection<ProductModel> LoadProduct()
        {
            ObservableCollection<ProductModel> localCollection = new ObservableCollection<ProductModel>();
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
           var selectedProduct=_selectedProduct;
            if (selectedProduct != null) {
                var productToUpdate = dc.Products.SingleOrDefault(p => p.ProductId == selectedProduct.ProductID);
                if (productToUpdate!=null)
                {
                    productToUpdate.ProductName = selectedProduct.ProductName;
                    productToUpdate.QuantityPerUnit = selectedProduct.QuantityPerUnit;
                    dc.SaveChanges();
                    
                }

            }
        }
        public ObservableCollection<TotalSalesByProductModel> TotalSalesByProductList
        {
            get { return _totalSalesByProductList ?? loadSalesByProduct(); }
        }

        private ObservableCollection<TotalSalesByProductModel> loadSalesByProduct()
        {
            ObservableCollection<TotalSalesByProductModel> localCollection=new ObservableCollection<TotalSalesByProductModel>();    

            foreach (var p in dc.Products) {

                var totalSales = dc.OrderDetails.Where(op => op.ProductId == p.ProductId)
                    .Sum(s => s.UnitPrice * s.Quantity);

                localCollection.Add(new TotalSalesByProductModel(p.ProductId, totalSales));
            
            }

            return localCollection;
        }
    }
}
