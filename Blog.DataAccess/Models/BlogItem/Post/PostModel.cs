using Blog.DataLibrary.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.DataLibrary.Models.BlogItem.Post
{
    public class PostModel : IPostModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
