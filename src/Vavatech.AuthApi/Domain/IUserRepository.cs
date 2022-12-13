namespace Vavatech.AuthApi.Domain;

public interface IUserRepository
{
    User GetByUsername(string username);
}
