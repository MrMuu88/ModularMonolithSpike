using Auth.Abstractions;

namespace Auth.Services
{
    public class MockAuthService : IAuthService
    {
        public string Login(string user, string passwd)
        {
            return "this is a mock token";
        }
    }
}
