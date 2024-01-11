using examen_janvier.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace examen_janvier.ViewsModels
{
    public class ProductModel :INotifyPropertyChanged
    {

        private readonly Product? _product;
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ProductModel(Product product)
        {
            this._product = product;
        }

        public Product? Product { get { return _product;} }

        public int? ProductId
        {
            get { return _product.ProductId; }
            set { _product.ProductId = (int)value;OnPropertyChanged(nameof(ProductId)); }
           
        }
        public string? ProductName
        {
            get { return _product.ProductName; }
            set { _product.ProductName = value; OnPropertyChanged(nameof(ProductName)); }
        }
        public string? Category
        {
            get { return _product.Category.CategoryName; }
            set { _product.Category.CategoryName = value; OnPropertyChanged(nameof(Category)); }
        }

        public string? Fournisseur
        {
            get {return _product.Supplier.ContactName; }
            set { _product.Supplier.ContactName = value; OnPropertyChanged(nameof(Fournisseur)); }

        }

       
    }
}
