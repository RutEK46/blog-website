using Blog.DataLibrary.Models;
using System.Threading.Tasks;

namespace Blog.DataLibrary.BusinessLogic
{
    public interface IAccountProcessor
    {
        Task<int> Create(string userName, string email, string salt, string passwordHash);
        Task<User> LoadByEmail(string email);
    }
}