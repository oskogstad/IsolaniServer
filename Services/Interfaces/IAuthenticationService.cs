using System.Threading.Tasks;
using Isolani.Models;
using Microsoft.IdentityModel.Tokens;

namespace Isolani.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<SecurityToken> GetTokenAsync(TokenRequest tokenRequest);
    }
}