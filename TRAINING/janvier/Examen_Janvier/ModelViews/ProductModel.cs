using Examen_Janvier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen_Janvier.ModelViews
{
    public class ProductModel
    {
        private readonly Product _product;
        private int? _count;
        private string? _country;

        public ProductModel(Product product)
        {
            _product = product;
        }
        public ProductModel( string country, int count)
        {
            _country = country;
            _count = count;

        }
        public Product? Product
        {
            get { return _product; }
        }

        
       
        public int ProductId
        {
            get { return _product.ProductId; }
            set { _product.ProductId = value; }
                        }
        public string? ProductName
        {
            get { return _product.ProductName; }
            set { _product.ProductName = value; }
        }
        public string Category
        {
            get { return _product.Category.CategoryName; }
            set { _product.Category.CategoryName = value; }
        }
        public string? Fournisseur
        {
            get { return _product.Supplier.ContactName; }
            set { _product.Supplier.ContactName = value; }
        }

        public int? Count { get => _count; set => _count = value; }

        public string Country
        {
            get { return _country; }
            set {


                _country = value;
              
            }
        }
    }
}
