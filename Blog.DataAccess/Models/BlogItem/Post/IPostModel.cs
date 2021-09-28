using Blog.DataLibrary.BusinessLogic;
using Blog.DataLibrary.Models.BlogItem;

namespace Blog.DataLibrary.Models.BlogItem.Post
{
    public interface IPostModel : IBlogItem
    {
        string Body { get; set; }
    }
}