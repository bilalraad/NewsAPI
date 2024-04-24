using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAPI.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}