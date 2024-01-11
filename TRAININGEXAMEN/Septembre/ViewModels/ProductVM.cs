using Septembre.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Septembre.ViewModels;
using System.ComponentModel;

namespace Septembre.ViewModels
{
    public class ProductVM
    {
        private NorthwindContext dc=new NorthwindContext();
        private ObservableCollection<ProductModel> _productsList;
        private ProductModel _selectedProduct;
        private DelegateCommand _majProduct;

        private ObservableCollection<SaleByProductModel> _saleByProduct;
      


        public ProductModel SelectedProduct {
            get { return _selectedProduct; }
            set { _selectedProduct = value;  }
        }

        public ObservableCollection<ProductModel> ProductsList
        {
            get { return _productsList ?? loadProductsList(); }
        }

        private ObservableCollection<ProductModel> loadProductsList()
        {
            ObservableCollection<ProductModel> localCollection =new ObservableCollection<ProductModel> ();
            foreach(var p in dc.Products)
            {
                localCollection.Add(new ProductModel(p));
            }
            return localCollection;
        }

        public DelegateCommand MajCommand
        {
            get { return _majProduct=_majProduct??new DelegateCommand(UpdateProduct); }
        }

        private void UpdateProduct()
        {
            var selectedProduct = _selectedProduct;

            if (selectedProduct != null) {
                var productInDb = dc.Products.FirstOrDefault(p => p.ProductId == selectedProduct.ProductID);

                selectedProduct.ProductName= productInDb.ProductName ;
                selectedProduct.QuantityPerUnit= productInDb.QuantityPerUnit  ;
                dc.SaveChanges();
               

             }
        }

        public ObservableCollection<SaleByProductModel> SaleByProduct
        {
            get { return _saleByProduct ?? loadSaleByProduct(); }
        }

        private ObservableCollection<SaleByProductModel> loadSaleByProduct()
        {
            ObservableCollection<SaleByProductModel> localCollection=new ObservableCollection<SaleByProductModel> ();

            var productList=dc.Products.Distinct().ToList();

            foreach(var p in productList)
            {
                var sum=dc.OrderDetails.Where(od=>od.Product.ProductId==p.ProductId).Sum(od=>od.UnitPrice*od.Quantity);
                localCollection.Add(new SaleByProductModel(p.ProductId,sum));

            }

            return localCollection;

        }
    }
}
