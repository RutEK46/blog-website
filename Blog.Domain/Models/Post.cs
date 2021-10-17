using System.ComponentModel.DataAnnotations;

namespace Blog.Domain.Models
{
    public class Post : BlogItem
    {
        public string Body { get; set; }
    }
}
