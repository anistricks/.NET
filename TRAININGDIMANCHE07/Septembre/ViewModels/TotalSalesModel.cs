using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Septembre.ViewModels
{
    public class TotalSalesModel
    {
        private int _idProduct;
        private decimal _totalSales;

        public TotalSalesModel(int idProduct, decimal totalSales)
        {
          IdProduct = idProduct;
          TotalSales= totalSales;
        }

        public int IdProduct { get => _idProduct; set => _idProduct = value; }
        public decimal TotalSales { get => _totalSales; set => _totalSales = value; }
    }
}
