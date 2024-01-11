using Septembre.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Septembre.ModelViews
{
    public class ProductModel
    {
        private readonly Product _product;
        private decimal _totalSales;
        private int? _productID;


        public ProductModel(Product product)
        {
            _product = product;
        }
        public ProductModel(int productID,decimal totalSales)
        {
            _productID=productID;
            _totalSales=totalSales;
        }

        public int ProductID
        {
            get { return _product.ProductId; }
            set { _product.ProductId = value; }
        }
        public string ProductName
        {
            get { return _product.ProductName; }
            set { _product.ProductName = value; }
        }
        public string SupplierContactName
        {
            get { return _product.Supplier.ContactName; }
            set { _product.Supplier.ContactName = value; }
        }
        public string QuantityPerUnit
        {
            get { return _product.QuantityPerUnit; }
            set { _product.QuantityPerUnit = value; }
        }
        public decimal TotalSales
        {
            get { return _totalSales; }
            set { _totalSales = value; }
        }







    }
}
