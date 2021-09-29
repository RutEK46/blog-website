using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.DataLibrary.Models
{
    public interface IAuthenticable
    {
        string Email { get; set; }
        string PasswordHash { get; set; }
        string Salt { get; set; }
    }
}
