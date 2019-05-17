using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Models
{
    public class Product
    {
        public Product()
        {
            OrderString = new HashSet<OrderString>();
        }
        public int ProductId { get; set; }
        //public int OrderId { get; set; }
        public string Name { get; set; }
        public int Costs { get; set; }
        public string Image { get; set; }

        //  public virtual Order Order { get; set; }
        public virtual ICollection<OrderString> OrderString { get; set; }

    }
}
