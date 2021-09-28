using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.DataLibrary.DataAccess
{
    public interface ISqlDataAccess
    {
        Task<IEnumerable<T>> LoadData<T>(string sql, object param = null);
        Task<int> SaveData<T>(string sql, T data);
    }
}