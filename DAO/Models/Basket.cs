using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Computer_Store.DAO.Models
{
    public enum status {Оплачено=1, Не_оплачено, Частично_оплачено }
    public class Basket
    {
        public int id { get; set; }
        public int clientId{ get; set; }
        public DateTime dateOfCreation { get; set; }
        public int statusId { get; set; }
        public int totalPrice { get; set; }
    }
}
