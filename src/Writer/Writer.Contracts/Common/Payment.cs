using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Writer.Contracts.Common
{
    public record Payment
    {
        public required string Identifier { get; init; }
    }
}
