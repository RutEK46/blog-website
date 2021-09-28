namespace Blog.DataLibrary.BusinessLogic
{
    public interface IPasswordHasher
    {
        string GetHash(string password, string salt);
        string GetRandomSalt();
    }
}