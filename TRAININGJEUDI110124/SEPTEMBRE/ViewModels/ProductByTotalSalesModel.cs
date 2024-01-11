using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPTEMBRE.ViewModels
{
    public class ProductByTotalSalesModel
    {
        private int _idOfProduct;
        private decimal _totalSales;

        public ProductByTotalSalesModel(int idOfProduct, decimal totalSales)
        {
            IdOfProduct = idOfProduct;
            _totalSales = totalSales;
        }

        public int IdOfProduct { get => _idOfProduct; set => _idOfProduct = value; }
        public decimal TotalSales { get => _totalSales; set => _totalSales = value; }
    }
}
