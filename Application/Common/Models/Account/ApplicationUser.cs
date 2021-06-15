using System;

namespace Application.Common.Models.Account
{
    public class ApplicationUser
    {
        public Guid Id { get; init; }

        public string Email { get; init; }
    }
}