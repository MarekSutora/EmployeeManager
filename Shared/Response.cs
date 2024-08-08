using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Response
    {
        public bool IsSuccess { get; set; }
        public required string Message { get; set; }
    }
}
