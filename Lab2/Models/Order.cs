using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Models
{
    //у одного заказа есть несколько товаров
    //коллекция строк заказов в заказе
    //коллекция строк заказов в товаре
    //заказ к строкам заказа 1 ко мног /строка заказа пост, блок ост
    //товар 1 много строк
    public partial class Order
        {
            public Order()
            {
            OrderString = new HashSet<OrderString>();
            }

        [Key] //ключево поле
            public int OrderId { get; set; }
        [Required] //обязательное свойство

        public DateTime Date { get; set; }
        public int Sum { get; set; }
        public int Act { get; set; }
        public string UserId { get; set; }
        public virtual ICollection<OrderString> OrderString { get; set; }
            public virtual User User { get; set; }
    }
    
}
