namespace Vavatech.AuthApi.Domain;

public interface IAuthService
{
    bool TryAuthorize(string username, string password, out User user);
}
