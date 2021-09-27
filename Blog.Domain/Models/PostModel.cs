using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Domain.Models
{
    public class PostModel : IPostModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
