using Blog.DataLibrary.BusinessLogic;

namespace Blog.DataLibrary.Models
{
    public interface IPostModel : IBlogItem
    {
        string Body { get; set; }
    }
}