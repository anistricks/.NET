using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPTEMBRE.ViewModels
{
    public class SalesProductModel
    {
        private int _idOfProduct;
        private decimal _totalSales;

        public SalesProductModel(int idOfProduct,decimal totalSales)
        {
            _idOfProduct = idOfProduct;
            _totalSales= totalSales;
        }

        public int IdOfProduct { get => _idOfProduct; set => _idOfProduct = value; }
        public decimal TotalSales { get => _totalSales; set => _totalSales = value; }
    }
}
