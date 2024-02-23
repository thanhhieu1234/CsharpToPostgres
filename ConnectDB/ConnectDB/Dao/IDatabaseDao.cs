using ConnectDB.Models;
using System.Runtime.InteropServices;

namespace ConnectDB.Repository
{
    public interface IDatabaseDao<T>
    {
        List<T> query(string sql);
        long insertOrUpdate(string sql, params object[] values);
    }
}
