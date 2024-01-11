using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace janvier.ViewModels
{   public class AffichageModel
    {
        private string _country;
        private int _count;
        public AffichageModel(string country,int count)
        {
            Country = country;
            Count = count;
        }

        public string Country { get => _country; set => _country = value; }
        public int Count { get => _count; set => _count = value; }
    }
}
