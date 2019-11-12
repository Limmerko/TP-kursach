using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_Store.DAO
{
    interface IDAO<T>
    {
        void create(T t);
        List<T> getAll();
        T getOne(int id);
        void update(int id, T t);
        void delete(int id);
    }
}
