using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.UI.Models.Post
{
    public class CreateViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [Required(ErrorMessage = "The post needs to have a body.")]
        public string Body { get; set; }
    }
}
