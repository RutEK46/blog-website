using Blog.DataLibrary.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.DataLibrary.Models.BlogItem.Post
{
    public class Post : BlogItem, IPost
    {
        public string Body { get; set; }
    }
}
