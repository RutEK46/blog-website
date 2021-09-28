using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.UI.Models.Post
{
    public class IndexViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }
    }
}
