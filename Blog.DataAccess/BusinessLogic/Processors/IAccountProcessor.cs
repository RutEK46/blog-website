using Blog.DataLibrary.Models;
using System.Threading.Tasks;

namespace Blog.DataLibrary.BusinessLogic.Processors
{
    public interface IAccountProcessor
    {
        Task<int> Create(string userName, string email, string salt, string passwordHash);
        Task<IAuthenticableUser> Load(string email);
    }
}