using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPTEMBRE.ViewModels
{
    public class ProductByTotalSales
    {
        private int _idOfProduct;
        private decimal _total;

        public ProductByTotalSales(int idOfProduct, decimal total)
        {
            _idOfProduct = idOfProduct;
            _total= total;

    }

    public int IdOfProduct { get => _idOfProduct; set => _idOfProduct = value; }
        public decimal Total { get => _total; set => _total = value; }
    }
}
