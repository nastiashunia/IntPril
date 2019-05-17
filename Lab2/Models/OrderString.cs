using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Models
{
    public class OrderString
    {
        public int OrderStringId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public int Cost { get; set; }

          public virtual Product Product { get; set; }
          public virtual Order Order { get; set; }

    }
}
