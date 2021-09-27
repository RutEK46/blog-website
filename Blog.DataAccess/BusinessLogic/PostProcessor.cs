using Blog.DataLibrary.DataAccess;
using Blog.DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataLibrary.BusinessLogic
{
    public class PostProcessor : IPostProcessor
    {
    
        private ISqlDataAccess _sqlDataAccess;

        public PostProcessor(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }

        public async Task<int> Create(string title, string body)
        {
            IPostModel data = new PostModel
            {
                Title = title,
                Body = body
            };

            string sql = @"insert into dbo.Post (Title, Body)
                            values (@Title, @Body);";

            return await _sqlDataAccess.SaveDataAsync(sql, data);
        }

        public async Task<IEnumerable<IPostModel>> Load()
        {
            string sql = @"select Id, Title, Body
                            from dbo.Post;";

            return await _sqlDataAccess.LoadDataAsync<PostModel>(sql);
        }

        public async Task<IPostModel> Load(int id)
        {
            string sql = @$"select Id, Title, Body
                            from dbo.Post
                            where Id = @Id;";

            var result = await _sqlDataAccess.LoadDataAsync<PostModel>(sql, new { Id = id });
            return result.FirstOrDefault();
        }

        public async Task<IPostModel> Load(string title)
        {
            string sql = @$"select Id, Title, Body
                            from dbo.Post
                            where Title = @Title;";

            var result = await _sqlDataAccess.LoadDataAsync<PostModel>(sql, new { Title = title });
            return result.FirstOrDefault();
        }

        public async Task<int> Update(int id, string title, string body)
        {
            IPostModel data = new PostModel
            {
                Id = id,
                Title = title,
                Body = body
            };

            string sql = $@"update dbo.Post
                            set Title = @Title, Body = @Body
                            where Id = @Id;";

            return await _sqlDataAccess.SaveDataAsync(sql, data);
        }

        public async Task<int> Delete(int id)
        {
            string sql = $@"delete
                            from dbo.Post
                            where Id = @Id;";

            return await _sqlDataAccess.SaveDataAsync(sql, new { Id = id });
        }
    }
}
