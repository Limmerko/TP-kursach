using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_Store.DAO
{
    interface IDAO
    {
        void create<T>(T t);
        List<T> getAll<T>();
        T getOne<T>(int id);
        void update<T>(int id, T t);
        void delete<T>(int id);
    }
}
