using System.Collections.Generic;

namespace Contracts
{
    public class ServiceResponse
    {
        public bool Success { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}
