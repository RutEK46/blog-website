using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.DataLibrary.Models
{
    public class AuthenticableUser : IAuthenticableUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Salt { get; set; }
        public string PasswordHash { get; set; }
    }
}
