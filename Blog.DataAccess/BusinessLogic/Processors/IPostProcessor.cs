using Blog.DataLibrary.Models.BlogItem;
using Blog.DataLibrary.Models.BlogItem.Post;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.DataLibrary.BusinessLogic.Processors
{
    public interface IPostProcessor
    {
        Task<int> Create(string title, int userId, string body);
        Task<int> Create(string title, string userName, string body);
        Task<int> Update(int id, string title, string body);
        Task<int> Delete(int id);
        Task<IEnumerable<IPost>> Load();
        Task<IPost> Load(int id);
        Task<IPost> Load(string title);

        Task<IEnumerable<I>> Load<I, T>()
            where I : IPost
            where T : class, I;

        Task<I> Load<I, T>(int id)
            where I : IPost
            where T : class, I;

        Task<I> Load<I, T>(string title)
            where I : IPost
            where T : class, I;
    }
}