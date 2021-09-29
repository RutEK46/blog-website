using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.DataLibrary.Models.BlogItem
{
    public interface IBlogItem
    {
        int Id { get; set; }
        string Title { get; set; }
        IUser Creator { get; set; }
        DateTime Created { get; set; }
    }
}
