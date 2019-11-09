using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Computer_Store.DAO.Models
{
    public class Basket
    {
        public int ID { get; set; }
        public int ShoppingList { get; set;}
        public int OrderList { get; set; }
        public int Id_Client { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string Status { get; set; }
        public int TotalPrice { get; set; }

    }
}
