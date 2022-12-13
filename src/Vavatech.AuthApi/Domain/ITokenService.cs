namespace Vavatech.AuthApi.Domain;

public interface ITokenService
{
    string Create(User user);
}
