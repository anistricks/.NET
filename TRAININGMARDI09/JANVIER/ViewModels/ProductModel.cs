using JANVIER.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace JANVIER.ViewModels
{
    public class ProductModel
    {
        private readonly Product _product;

        public ProductModel(Product product)
        {
            _product = product;
        }

        public Product Product => _product;

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
        public string Category
        {
            get { return _product.Category.CategoryName; }
            set { _product.Category.CategoryName = value; }
        }
        public string Fournisseur
        {
            get { return _product.Supplier.ContactName; }
            set { _product.Supplier.ContactName = value; }
        }
    }
}
