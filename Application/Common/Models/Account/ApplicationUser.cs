using System;

namespace Application.Common.Models.Account
{
    public class ApplicationUser
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public string Email { get; set; }
    }
}