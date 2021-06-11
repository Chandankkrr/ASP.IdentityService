using System;

namespace Contracts.Responses.Account
{
    public class GetUserResponse: ServiceResponse
    {
        public Guid Id { get; set; }
        
        public string Email { get; set; }
    }
}