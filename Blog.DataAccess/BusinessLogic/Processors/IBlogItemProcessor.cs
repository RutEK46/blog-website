using Blog.DataLibrary.Models.BlogItem;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.DataLibrary.BusinessLogic.Processors
{
    public interface IBlogItemProcessor
    {
        Task<int> Create(string title, int userId);
        Task<int> Create(string title, string userName);
        Task<int> Update(int id, string title);
        Task<IEnumerable<IBlogItem>> Load();
        Task<IBlogItem> Load(int id);
        Task<IBlogItem> Load(string title);

        Task<IEnumerable<I>> Load<I, T>()
            where I : IBlogItem
            where T : class, I;

        Task<I> Load<I, T>(int id)
            where I : IBlogItem
            where T : class, I;

        Task<I> Load<I, T>(string title)
            where I : IBlogItem
            where T : class, I;
    }
}