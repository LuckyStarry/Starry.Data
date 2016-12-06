using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Starry.Data
{
    public interface IDbClientQueryPageable : IDbClient
    {
        IEnumerable<T> Query<T>(string sqlText, out int total, int pageIndex = 1, int pageSize = 20, object param = null);
        IEnumerable<T> Query<T>(string sqlText, object param, out int total, int pageIndex = 1, int pageSize = 20);
    }
}
