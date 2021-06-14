using System;

namespace Contracts.Account.Responses
{
    public class GetUserResponse: ServiceResponse
    {
        public Guid Id { get; set; }
        
        public string Email { get; set; }
    }
}