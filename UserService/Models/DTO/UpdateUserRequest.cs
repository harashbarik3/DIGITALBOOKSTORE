﻿namespace UserService.Models.DTO
{
    public class UpdateUserRequest
    {  
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string? UserType { get; set; } = null!;
        public string? FirstName { get; set; } = null!;
        public string? LastName { get; set; } = null!;
    }
}
