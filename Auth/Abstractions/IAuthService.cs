namespace Auth.Abstractions
{
    public interface IAuthService
    {
        string Login(string user, string passwd);
    }
}
