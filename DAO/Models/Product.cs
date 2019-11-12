using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Computer_Store.DAO.Models
{
    public enum category {
        Процессор = 1, Видеокарта, ОП, Блок_питания, Корпус, HDD, SSD,
        Монитор, Клавиатура, Мышь, Веб_камера, Графический_планшет
    }

    public class Product
    {
        public int id { get; set; }
        public string title { get; set; }
        public string number { get; set; }
        public int categoryId { get; set; }
        public string producer { get; set; }
        public int price { get; set; }
        public int amount { get; set; }
    }
}
