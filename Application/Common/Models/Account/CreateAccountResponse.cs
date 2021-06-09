using System;

namespace Application.Common.Models.Account
{
    public class CreateAccountResponse
    {
        public Guid Id { get; set; }
        
        public string Email { get; set; }
        
        public bool Success { get; set; }
    }
}