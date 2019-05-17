using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Lab2.Models
{
    public class User: IdentityUser
    {
        public int Role { get; set; }
        public int OrderId { get; set; }
        public User()
        {
            Order = new HashSet<Order>();
        }
        public virtual ICollection<Order> Order { get; set; }
    }
}
