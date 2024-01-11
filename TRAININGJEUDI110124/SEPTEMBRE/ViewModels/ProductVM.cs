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
        private ObservableCollection<ProductByTotalSalesModel> _productByTotalSalesList;
        private ProductModel _selectedProduct;
        private DelegateCommand _majCommand;

        public ProductModel SelectedProduct { get => _selectedProduct; set => _selectedProduct = value; }

        public ObservableCollection<ProductModel> ProductsList
        {
            get { return _productsList ?? LoadProductsList(); }
        }

        private ObservableCollection<ProductModel> LoadProductsList()
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

            var selectedProduct = _selectedProduct;
            if (selectedProduct != null)
            {
                var productToUpdate=dc.Products.SingleOrDefault(p=>p.ProductId == selectedProduct.ProductId);
                if (productToUpdate != null)
                {
                    productToUpdate.ProductName=selectedProduct.ProductName;
                    productToUpdate.QuantityPerUnit=selectedProduct.QuantityPerUnit;
                    dc.SaveChanges();

                }
            }
        }

        public ObservableCollection<ProductByTotalSalesModel> ProductByTotalSalesList
        {
            get { return _productByTotalSalesList ?? LoadProductByTotalSalesList();
            }
        }

        private ObservableCollection<ProductByTotalSalesModel> LoadProductByTotalSalesList()
        {
            ObservableCollection<ProductByTotalSalesModel> localCollection = new ObservableCollection<ProductByTotalSalesModel>();
            foreach (var item in dc.Products)

            {
                var total = dc.OrderDetails.Where(p => p.ProductId == item.ProductId)
                    .Sum(tot => tot.Quantity * tot.UnitPrice);

                localCollection.Add(new ProductByTotalSalesModel(item.ProductId, total));
            }
            return localCollection;
        }
    }
}
