using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Writer.Contracts.Common
{
    public record Organizer
    {
        public required string Email { get; init; }
    }
}
