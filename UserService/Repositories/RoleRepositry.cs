using CommonLib.Models;
using Microsoft.EntityFrameworkCore;

namespace UserService.Repositories
{
    public class RoleRepositry : IRoleRepositry
    {
        private readonly DigitalbookstoreContext _context;
        public RoleRepositry(DigitalbookstoreContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> AddRoleAsync(Role _role)
        {
            var role = new Role()
            {
                RoleId = Guid.NewGuid(),
                RoleName = _role.RoleName
            };

            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();

            return role;

        }
    }
}
