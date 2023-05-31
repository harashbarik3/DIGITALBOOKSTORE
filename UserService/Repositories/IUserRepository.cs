using CommonLib.Models;

namespace UserService.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUserAsync();
        Task<User> AddUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<User> RemoveUserAsync(Guid userid);
        Task<LoggedInUser> SignIn(Login login);
    }
}
