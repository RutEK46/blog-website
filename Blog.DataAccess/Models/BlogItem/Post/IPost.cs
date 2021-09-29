using Blog.DataLibrary.BusinessLogic;

namespace Blog.DataLibrary.Models.BlogItem.Post
{
    public interface IPost : IBlogItem
    {
        string Body { get; set; }
    }
}