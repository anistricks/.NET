using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JANVIER.ViewModels
{
    public class ProductBySalesCountryModel
    {
        private string _country;
        private int _numberProduct;

        public ProductBySalesCountryModel(string country, int numberProduct)
        {
            _country = country;
            _numberProduct = numberProduct;
        } 

       

        public string Country { get => _country; set => _country = value; }
        public int NumberProduct { get => _numberProduct; set => _numberProduct = value; }
    }
}
