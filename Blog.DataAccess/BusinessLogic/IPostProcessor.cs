using Blog.DataLibrary.Models.BlogItem;
using Blog.DataLibrary.Models.BlogItem.Post;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.DataLibrary.BusinessLogic
{
    public interface IPostProcessor
    {
        Task<int> Create(string title, string body);
        Task<IEnumerable<IBlogItem>> LoadBlogItems();
        Task<IPostModel> Load(int id);
        Task<IPostModel> Load(string title);
        Task<int> Update(int id, string title, string body);
        Task<int> Delete(int id);
    }
}