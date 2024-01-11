using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JANVIER.ViewModels
{
    public class ProductsByCountryModel
    {
        private string _country;
        private int _numberOfProduct;

        public ProductsByCountryModel(string country, int numberOfProduct)
        {
            _country = country;
            NumberOfProduct= numberOfProduct;
        }

        public string Country { get => _country; set => _country = value; }
        public int NumberOfProduct { get => _numberOfProduct; set => _numberOfProduct = value; }
    }
}
