using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Septembre.ViewModels
{
    public class TotalSalesByProductModel
    {
        private int _idOfProduct;
        private decimal _totalSales;

        public TotalSalesByProductModel(int idOfProduct, decimal totalSales)
        {
            _idOfProduct = idOfProduct;
            _totalSales= totalSales;
        }

        public decimal TotalSales { get => _totalSales; set => _totalSales = value; }
        public int IdOfProduct { get => _idOfProduct; set => _idOfProduct = value; }
    }
}
