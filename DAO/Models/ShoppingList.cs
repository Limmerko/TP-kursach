using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Computer_Store.DAO.Models
{
    public enum statusProduct
    {
        Оплачено=1, Не_оплачено, Заказано, Не_заказано, Доставлено
    }

    public class ShoppingList
    {
        public int id { get; set; }
        public int basketId { get; set; }
        public int productId { get; set; }
        public int statusId { get; set; }
    }
}
