using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Computer_Store.DAO.Models
{
    public class Client
    {
        public int id { get; set; }
        public string name { get; set; }
        public string patronymic { get; set; }
        public string surname { get; set; }
        public string phone { get; set; }
    }
}
