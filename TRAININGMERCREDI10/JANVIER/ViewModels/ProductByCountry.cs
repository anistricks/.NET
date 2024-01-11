using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JANVIER.ViewModels
{
    public class ProductByCountry
    {
        private string _country;
        private int _numberOfProduct;

        public ProductByCountry(string country, int numberOfProduct)
        {
            _country = country;
            _numberOfProduct = numberOfProduct; 
        }

        public string Country { get => _country; set => _country = value; }
        public int NumberOfProduct { get => _numberOfProduct; set => _numberOfProduct = value; }
    }
}
