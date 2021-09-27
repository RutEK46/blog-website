using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.DataLibrary.DataAccess
{
    public interface ISqlDataAccess
    {
        IEnumerable<T> LoadData<T>(string sql, object param = null);
        Task<IEnumerable<T>> LoadDataAsync<T>(string sql, object param = null);
        int SaveData<T>(string sql, T data);
        Task<int> SaveDataAsync<T>(string sql, T data);
    }
}