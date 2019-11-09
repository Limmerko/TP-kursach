using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Computer_Store.DAO.Models
{
    public class Basket
    {
        public int id { get; set; }
        public int shoppingList { get; set;}
        public int orderList { get; set; }
        public int clientId{ get; set; }
        public DateTime dateOfCreation { get; set; }
        public string status { get; set; }
        public int totalPrice { get; set; }

    }
}
