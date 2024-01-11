using examen_janvier.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace examen_janvier.ViewsModels
{
    public class ProductVM
    {
        private List<ProductModel> _products;
        private NorthwindContext dc=new NorthwindContext(); 

   

        public List<ProductModel> LoadProduct()
        {
            List<ProductModel> produit  = new List<ProductModel>();
            foreach (var p in dc.Products)
            {

              produit.Add(new ProductModel(p));


            }
            return produit;

        }

        public List<ProductModel> ProductsList { 
            get { return _products ?? LoadProduct(); }


        }





    }
}
