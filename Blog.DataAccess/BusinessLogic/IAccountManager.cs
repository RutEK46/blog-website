using System.Threading.Tasks;

namespace Blog.DataLibrary.BusinessLogic
{
    public interface IAccountManager
    {
        Task<bool> CreateAccount(string userName, string email, string password);
        Task SignInWithPassword(string email, string password);
        Task SignOut();
    }
}