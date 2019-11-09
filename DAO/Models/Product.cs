using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Computer_Store.DAO.Models
{
    public class Product
    {
        public string Title { get; set; }
        public string Number { get; set; }
        public string Category { get; set; }
        public string Producer { get; set; }
        public int Price { get; set; }
    }
}
