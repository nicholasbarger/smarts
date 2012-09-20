using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smarts.Api.Db
{
    internal interface IDbReadOnly<T> where T: class
    {
        T Get(int id);
        IQueryable<T> GetQuery();
    }
}
