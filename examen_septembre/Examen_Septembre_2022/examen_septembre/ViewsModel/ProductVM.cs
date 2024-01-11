using examen_septembre.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApplication1.ViewModels;

namespace examen_septembre.ViewsModel
{
    public class ProductVM 
    {
        ObservableCollection<ProductModel> _listproducts;
        NorthwindContext dc=new NorthwindContext();
        private ProductModel _selectedProduct;
        ObservableCollection<ProductModel> _listproductsDer;
   
        

        public ProductModel SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; }
        }
      


        public ObservableCollection<ProductModel> LoadProduct()
        {
            ObservableCollection<ProductModel> l = new ObservableCollection<ProductModel>();

            foreach (var item in dc.Products)
            {
                l.Add(new ProductModel(item));

            }

            return l;

        }


        public ObservableCollection<ProductModel> ListProducts
        {
            get { return _listproducts ?? LoadProduct(); }
            set
            {
                if (_listproducts != value)
                {
                    _listproducts = value;
                   
                }
            }
        }

      
        public ObservableCollection<ProductModel> CalculateTotalSalesByProduct()
        {
           var query = from product in dc.Products
                join orderDetail in dc.OrderDetails on product.ProductId equals orderDetail.ProductId
                group new { product, orderDetail } by product.ProductId into grouped
                select new ProductModel(grouped.First().product)
                {
                    ProductId = grouped.Key,
                    TotalSales = grouped.Sum(x => x.orderDetail.UnitPrice * x.orderDetail.Quantity),
                   
                };

        return new ObservableCollection<ProductModel>(query);
        }

        public ObservableCollection<ProductModel> ListproductsDer
        {
            get { return _listproductsDer ?? CalculateTotalSalesByProduct();}
        }


      


    }
}
