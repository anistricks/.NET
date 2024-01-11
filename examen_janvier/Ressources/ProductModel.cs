using examen_janvier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace examen_janvier.ViewsModels
{
    public class ProductModel
    {
        private readonly Product? _product;

        public ProductModel(Product product)
        {
            this._product = product;
        }

        public Product? Product { get { return _product;} }

        public int? ProductID
        {
            get { return _product.ProductId; }
        }
        public string? ProductName
        {
            get { return _product.ProductName; }
        }
        public string? Category
        {
            get { return _product.Category.CategoryName; }
        }

        public string? Fournisseur
        {
            get {return _product.Supplier.ContactName; }
        }



    }
}
