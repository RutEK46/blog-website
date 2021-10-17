using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.Domain.Models
{
    [Index(nameof(Title), IsUnique = true)]
    public class BlogItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 1)]
        public string Title { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
