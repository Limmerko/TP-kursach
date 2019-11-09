using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Computer_Store.DAO.Models
{
    public class Product
    {
        public string title { get; set; }
        public string number { get; set; }
        public string category { get; set; }
        public string producer { get; set; }
        public int price { get; set; }
    }
}
