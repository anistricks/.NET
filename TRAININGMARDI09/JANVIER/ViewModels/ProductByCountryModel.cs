using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JANVIER.ViewModels
{
    public class ProductByCountryModel
    {
        private string _country;
        private int _productByCountry;

        public ProductByCountryModel(string country, int productByCountry)
        {
            _country = country;
            _productByCountry = productByCountry;
        }

        public string Country { get => _country; set => _country = value; }
        public int ProductByCountry { get => _productByCountry; set => _productByCountry = value; }
    }
}
