namespace Blog.Domain.Models
{
    public interface IPostModel : IBlogItem
    {
        string Body { get; set; }
    }
}