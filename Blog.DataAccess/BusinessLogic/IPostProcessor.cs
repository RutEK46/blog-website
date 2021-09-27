using Blog.DataLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.DataLibrary.BusinessLogic
{
    public interface IPostProcessor
    {
        Task<int> Create(string title, string body);
        Task<IEnumerable<IPostModel>> Load();
        Task<IPostModel> Load(int id);
        Task<IPostModel> Load(string title);
        Task<int> Update(int id, string title, string body);
        Task<int> Delete(int id);
    }
}