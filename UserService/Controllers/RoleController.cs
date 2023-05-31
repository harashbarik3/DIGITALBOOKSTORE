using CommonLib.Models;
using Microsoft.AspNetCore.Mvc;
using UserService.Models.DTO;
using UserService.Repositories;

namespace UserService.Controllers
{
    [Route("api/v1/digitalbooks/[controller]")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly IRoleRepositry _roleRepositry;
        public RoleController(IRoleRepositry roleRepositry)
        {
            _roleRepositry = roleRepositry;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles=await _roleRepositry.GetAllRolesAsync();

            return Ok(roles);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole([FromBody] AddRoleRequest request)
        {
            var addRole = new Role()
            {                
                RoleName = request.RoleName,
            };

            var addRoleResult = await _roleRepositry.AddRoleAsync(addRole);

            return Ok(addRoleResult);
        }
    }
}
