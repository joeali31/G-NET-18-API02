using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce01.Application.Services.Contracts
{
    public interface IJwtService
    {
        string CreateTokenAsync(string userId , string email , string userName , IReadOnlyList<string> roles);
    }
}
