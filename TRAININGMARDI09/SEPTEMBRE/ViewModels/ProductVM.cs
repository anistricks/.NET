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
        private ObservableCollection<SalesProductModel> _salesProductsList;
        private ProductModel _selectedProduct;
        private DelegateCommand _majCommand;

        public ProductModel SelectedProduct { get => _selectedProduct; set => _selectedProduct = value; }

        public ObservableCollection<ProductModel> ProductsList
        {
            get { return _productsList ?? loadProductList(); }
        }

        private ObservableCollection<ProductModel> loadProductList()
        {
            ObservableCollection<ProductModel> localCollection = new ObservableCollection<ProductModel>();
            foreach (var product in dc.Products)
            {
                localCollection.Add(new ProductModel(product));

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
            if(selectedProduct != null)
            {
                var productToUpdate = dc.Products.SingleOrDefault(p => p.ProductId == selectedProduct.ProductID);
                productToUpdate.ProductName= selectedProduct.ProductName;   
                productToUpdate.QuantityPerUnit= selectedProduct.QuantityPerUnit;
                dc.SaveChanges();


            }
        }

        public ObservableCollection<SalesProductModel> SalesProductsList
        {
            get { return _salesProductsList ?? loadSalesProduct(); }
        }

        private ObservableCollection<SalesProductModel> loadSalesProduct()
        {
            ObservableCollection<SalesProductModel> localCollection=new ObservableCollection<SalesProductModel>(); 
            foreach(var product in dc.Products)
            {
                var totalSales = dc.OrderDetails.Where(p => p.ProductId == product.ProductId)
                    .Sum(t => t.UnitPrice * t.Quantity);

                localCollection.Add(new SalesProductModel(product.ProductId,totalSales));
            }
            return localCollection;
        }
    }
}
