using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Septembre.ViewModels
{
    public class SaleByProductModel
    {
        private decimal _count;
        private int _productId;

        public SaleByProductModel( int productId,decimal count)
        {

            _productId = productId;
            _count = count;
        }

        public decimal Count { get => _count; set => _count = value; }
        public int ProductId { get => _productId; set => _productId = value; }
    }
}
