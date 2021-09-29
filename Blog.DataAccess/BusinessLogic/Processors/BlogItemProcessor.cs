using Blog.DataLibrary.DataAccess;
using Blog.DataLibrary.Models.BlogItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataLibrary.BusinessLogic.Processors
{
    public class BlogItemProcessor : IBlogItemProcessor
    {
        private ISqlDataAccess _sqlDataAccess;

        public BlogItemProcessor(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }

        public Task<int> Create(string title, int userId)
            => _sqlDataAccess.SaveData(
                "spBlogItem_Insert @Title, @UserId, @Created", new
                {
                    Title = title,
                    UserId = userId,
                    Created = DateTime.Now
                });

        public Task<int> Create(string title, string userName)
            => _sqlDataAccess.SaveData(
                "spBlogItem_InsertByUserName @Title, @UserName, @Created", new
                {
                    Title = title,
                    UserName = userName,
                    Created = DateTime.Now
                });

        public async Task<IEnumerable<IBlogItem>> Load()
            => await Load<IBlogItem, BlogItem>();

        public async Task<IBlogItem> Load(int id)
            => await Load<IBlogItem, BlogItem>(id);

        public async Task<IBlogItem> Load(string title)
            => await Load<IBlogItem, BlogItem>(title);

        public async Task<IEnumerable<I>> Load<I, T>()
            where I : IBlogItem
            where T : class, I
            => await _sqlDataAccess.LoadData<T>("spBlogItem_Select");

        public async Task<I> Load<I, T>(int id)
            where I : IBlogItem
            where T : class, I
            => (await _sqlDataAccess.LoadData<T>(
                "spBlogItem_SelectById @Id", new
                {
                    Id = id
                }))
                .FirstOrDefault();

        public async Task<I> Load<I, T>(string title)
            where I : IBlogItem
            where T : class, I
            => (await _sqlDataAccess.LoadData<T>(
                "spBlogItem_SelectByTitle @Title", new
                {
                    Title = title
                }))
                .FirstOrDefault();

        public async Task<int> Update(int id, string title)
            => await _sqlDataAccess.SaveData(
                "spBlogItem_Update @Id, @Title", new
                {
                    Id = id,
                    Title = title
                });
    }
}
