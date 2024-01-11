using EXO10.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXO10.ViewModels
{
    public class OrderModel
    {
        private readonly Order _order;
        private decimal _orderTotal;

        public OrderModel(Order order, decimal orderTotal)
        {
            _order = order;
            _orderTotal = orderTotal;
        }

        public Order Order => _order;

        public string OrderDate
        {
            get { return _order.OrderDate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture); }
        }
        public decimal OrderTotal
        {
            get { return _orderTotal; }
        }

        public int OrderID
        {
            get { return _order.OrderId; }
        }
    }
}
