﻿using CommonLib.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace UserService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DigitalbookstoreContext _context;
        private IConfiguration _configuration;
        public UserRepository(DigitalbookstoreContext context, IConfiguration config)
        {
            _context = context;
            _configuration = config;
        }

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<User> AddUserAsync(User user)
        {
            try
            {
                var role = await _context.Roles.Where(x => x.RoleName.ToLower() == user.UserType.ToLower()).FirstOrDefaultAsync();
                user.UserId = Guid.NewGuid();
                user.RoleId = role.RoleId;
                await _context.AddAsync(user);
                await _context.SaveChangesAsync();

                return user;
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.UserId);

            if (existingUser != null)
            {
                existingUser.UserName = user.UserName;
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Email = user.Email;
                existingUser.UserType = user.UserType;
                existingUser.Password = user.Password;

                await _context.SaveChangesAsync();
            }

            return existingUser;
        }

        public async Task<User> RemoveUserAsync(Guid userid)
        {
            var existingUser = await _context.Users.FindAsync(userid);

            if(existingUser != null)
            {
                _context.Users.Remove(existingUser);

                await _context.SaveChangesAsync();                
            }

            return existingUser;

        }

        public async Task<LoggedInUser> SignIn(Login login)
        {
            string token = string.Empty;
            var tokenString = token;

            try
            {
                var loggedInUser= AuthenticateUser(login);

                if (loggedInUser != null)
                {
                    tokenString= GenerateToken(loggedInUser);

                    var _loggedUser = new LoggedInUser()
                    {
                        token = tokenString,
                        user = loggedInUser
                    };

                    return _loggedUser;
                }

                return new LoggedInUser
                {
                    token = tokenString,
                    user = loggedInUser
                };
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        private User AuthenticateUser(Login login)
        {
            var user = new User();

            try
            {
                user = _context.Users.Where(x => x.UserName == login.Userid && x.Password == login.Password).FirstOrDefault();

                if (user != null)
                {
                    return user;
                }
                else
                {
                    return null;
                }

            }
            catch(Exception ex)
            {
                return null;
            }
        }

        private string GenerateToken(User login)
        {
            try
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,login.UserName),
                    new Claim(ClaimTypes.Role, login.UserType)
                };

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:Key"]));

                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["jwt:Issuer"],
                    _configuration["jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials
                    );
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch
            {
                return null;
            }
        }
    }
}