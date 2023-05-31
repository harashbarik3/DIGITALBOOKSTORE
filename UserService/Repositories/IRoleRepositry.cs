using CommonLib.Models;

namespace UserService.Repositories
{
    public interface IRoleRepositry
    {
        Task<IEnumerable<Role>> GetAllRolesAsync();

        Task<Role> AddRoleAsync(Role _role);
    }
}
