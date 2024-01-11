using examen_septembre.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace examen_septembre.ViewsModel
{
    public class ProductModel
    {
        private readonly Product? _product;
        private int _productId;
        private decimal _totalSales;


    public ProductModel(Product product)
        {
            _product = product;
        }
   
        public decimal TotalSales
        {
            get { return _totalSales; }
            set {  _totalSales=value; }
        }

        public int ProductId
        {
            get {return _product.ProductId ; }
            set {
                _product.ProductId = value;
            }
        }
        public string ProductName
        {
            get { return _product.ProductName; }
            set {  _product.ProductName=value; }

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




    }
}
