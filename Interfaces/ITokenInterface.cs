using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace NewsAPI.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);

        (byte[], byte[]) GenerateHash(string password, [Optional] byte[]? salt);
    }
}