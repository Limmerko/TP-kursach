using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Computer_Store.DAO.Models
{
    public class OrderList
    {
        public int id { get; set; }
        public int basketId { get; set; }
        public int productId { get; set; }
    }
}
