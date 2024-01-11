using examen_janvier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace examen_janvier.ViewModel
{
    public class OrderModel
    {
        private readonly Order? _monOrder;
        private int? _count = 0;

       
        public OrderModel(Order monOrder,int count) {

            _monOrder=monOrder;
            _count = count;
        }

     public String? Country
        {
            get
            {
                return _monOrder.ShipCountry;

            }
        }

        public int? Count
        {
            get
            {
                return _count;

            }
        }
    }
}
