using App.Core.RequestFlow;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Contract
{
    public interface IIdentityServices
    {
        Task<AuthenticationResult> RegisterAsync(UserRegistrationRequest model);
        Task<AuthenticationResult> LoginAsync(string email, string password);
        Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);
        Task<string> GetUser(string token);
    }
}
