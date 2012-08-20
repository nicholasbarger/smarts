using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smarts.Api.Db
{
    internal interface IDbCrud<T> where T: class
    {
        bool Delete(int id);
        T Get(int id);
        IQueryable<T> GetQuery();
        bool Save(ref T obj);
    }
}
