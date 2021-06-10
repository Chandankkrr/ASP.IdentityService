using System.Collections.Generic;

namespace Contracts.Responses.Account
{
    public class ServiceResponse
    {
        public bool Success { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}
