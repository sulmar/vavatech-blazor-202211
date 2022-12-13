using Vavatech.AuthApi.Domain;

namespace Vavatech.AuthApi.Infrastructure;

public class InMemoryUserRepository : IUserRepository
{
    private IDictionary<int, User> _users;

    public InMemoryUserRepository(IEnumerable<User> users)
    {
        _users = users.ToDictionary(p => p.Id);
    }

    public User GetByUsername(string username)
    {
        return _users.Values.SingleOrDefault(u => u.Username.Equals(username));
    }
}
