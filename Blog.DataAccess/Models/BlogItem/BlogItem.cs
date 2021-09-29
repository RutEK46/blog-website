using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.DataLibrary.Models.BlogItem
{
    public class BlogItem : IBlogItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IUser Creator { get; set; }
        public DateTime Created { get; set; }
    }
}
