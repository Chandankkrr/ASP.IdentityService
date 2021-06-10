using System.Collections.Generic;

namespace Application.Common.Models
{
    public class Result
    {
        public bool Success { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}
