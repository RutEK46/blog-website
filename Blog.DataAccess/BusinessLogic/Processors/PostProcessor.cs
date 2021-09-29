using Blog.DataLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.DataLibrary.Models.BlogItem.Post;
using Blog.DataLibrary.Models.BlogItem;

namespace Blog.DataLibrary.BusinessLogic.Processors
{
    public class PostProcessor : IPostProcessor
    {
        private ISqlDataAccess _sqlDataAccess;

        public PostProcessor(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }

        public async Task<int> Create(string title, int userId, string body)
            => await _sqlDataAccess.SaveData(
                "spPost_Insert @Title, @UserId, @Created, @Body", new
                {
                    Title = title,
                    UserId = userId,
                    Body = body,
                    Created = DateTime.Now
                });

        public async Task<int> Create(string title, string userName, string body)
            => await _sqlDataAccess.SaveData(
                "spPost_InsertByUserName @Title, @UserName, @Created, @Body", new
                {
                    Title = title,
                    UserName = userName,
                    Body = body,
                    Created = DateTime.Now
                });

        public async Task<int> Update(int id, string title, string body)
            => await _sqlDataAccess.SaveData(
                "spPost_Update @Id, @Title, @Body", new
                {
                    Id = id,
                    Title = title,
                    Body = body
                });

        public async Task<int> Delete(int id)
            => await _sqlDataAccess.SaveData(
                "spPost_Delete @Id", new
                {
                    Id = id
                });

        public async Task<IEnumerable<IPost>> Load()
            => await Load<IPost, Post>();

        public async Task<IPost> Load(int id)
            => await Load<IPost, Post>(id);

        public async Task<IPost> Load(string title)
            => await Load<IPost, Post>(title);

        public async Task<IEnumerable<I>> Load<I, T>()
            where I : IPost
            where T : class, I
            => await _sqlDataAccess.LoadData<T>("spPost_Select");

        public async Task<I> Load<I, T>(int id)
            where I : IPost
            where T : class, I
            => (await _sqlDataAccess.LoadData<T>("spPost_SelectById @Id", new
            {
                Id = id
            }))
            .FirstOrDefault();

        public async Task<I> Load<I, T>(string title)
            where I : IPost
            where T : class, I
            => (await _sqlDataAccess.LoadData<T>("spPost_SelectByTitle @Title", new
            {
                Title = title
            }))
            .FirstOrDefault();
    }
}
