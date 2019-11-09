using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_Store.DAO
{
    interface IDAO
    {
        void Create<T>(T t);
        List<T> GetAll<T>();
        T GetOne<T>(int id);
        void Update<T>(int id, T t);
        void Delete<T>(int id);
    }
}
