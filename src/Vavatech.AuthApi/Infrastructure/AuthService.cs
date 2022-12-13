using Vavatech.AuthApi.Domain;

namespace Vavatech.AuthApi.Infrastructure;

public class AuthService : IAuthService
{
    private readonly IUserRepository userRepository;

    public AuthService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public bool TryAuthorize(string username, string password, out User user)
    {
        user = userRepository.GetByUsername(username);

        // TODO: hash password
        return user != null && user.HashedPassword == password;
    }
}
