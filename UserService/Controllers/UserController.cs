using CommonLib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using UserService.Models;
using UserService.Models.DTO;
using UserService.Repositories;

namespace UserService.Controllers
{
    [Route("api/v1/digitalbooks")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("")]
        [Authorize(Roles = "AUTHER")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userRepository.GetAllUserAsync();
                
                return Ok(users);
            }
            catch (Exception ex)
            {
                return NotFound("There is no user available");
            }
                       
        }

        [HttpPost("user/Register")]
        public async Task<IActionResult> CreateUserAsync([FromBody] AddUserRequest user)
        {
            try
            {
                var addUser = new User()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserType = user.UserType,
                    Password = user.Password
                };
                var userAdd=await _userRepository.AddUserAsync(addUser);

                return Ok(userAdd);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("update/user/{userId:guid}")]
        public async Task<ActionResult<User>> UpdateUser([FromRoute]Guid userId, [FromBody] UpdateUserRequest updateUserRequest)
        {
            try
            {
                var userToUpdate = new User
                {
                    UserId = userId,
                    UserName = updateUserRequest.UserName,
                    Email = updateUserRequest.Email,
                    FirstName = updateUserRequest.FirstName,
                    LastName = updateUserRequest.LastName,
                    UserType = updateUserRequest.UserType,
                    Password = updateUserRequest.Password
                };

                var updatedUser = await _userRepository.UpdateUserAsync(userToUpdate);

                if (updatedUser == null)
                {
                    return NotFound();
                }

                return Ok(updatedUser);
            }
            catch
            {
                return NotFound();
            }
            
        }

        [HttpDelete("delete/user/{userId:guid}")]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] Guid userId)
        {
            try
            {
                var user = await _userRepository.RemoveUserAsync(userId);
                return Ok("The user has been deleted successfully");               
                
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost("user/Login")]
        public async Task<IActionResult> LogIn(Login login)
        {
            try
            {
                var token = await _userRepository.SignIn(login);

                if(token != null)
                {
                    return Ok(token.token);
                }
                else
                {
                    throw new Exception();
                }
                
            }
            catch(Exception ex)
            {
                return BadRequest("Please Check username and password");
            }
        }

      
    }
}
