using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAPI.DTOs
{
    public class AuthDto
    {
        public required string Token { get; set; }
        public required UserDto User { get; set; }
    }
}