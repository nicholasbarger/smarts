using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smarts.Api.Db
{
    internal interface IDbCrud<T> where T: class
    {
        void Delete(ref T obj);
        void Delete(int id);
        T Get(int id);
        IQueryable<T> GetQuery();
        void Save(ref T obj);
        List<T> Search(string q);
        IQueryable<T> SearchQuery(string q);
    }
}
