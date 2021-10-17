using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Domain.Models
{
    public class User : IdentityUser
    {
        public IList<BlogItem> BlogItems { get; } = new List<BlogItem>();
    }
}
